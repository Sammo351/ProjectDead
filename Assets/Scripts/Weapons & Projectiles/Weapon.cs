using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject Bullet;
    public Transform SpawnPoint;
    public int ammo, maxAmmo, clipSize, reloadTime, currentClip;
    public float fireRate; // how shots per second
    float inverseFireRate; // how many seconds per shot
    bool startFiring = false, reloading = false;
    float cooldown = 0; // how long until can fire next shot automatically 

    void Start () {
        QuickSet ();
    }
    //_reloadTime in milliseconds, _fireRate is shots per second
    public void QuickSet (int _ammo = 90, int _clipSize = 30, int _reloadTime = 2500, float _fireRate = 5) {
        ammo = _ammo;
        maxAmmo = _ammo;
        clipSize = _clipSize;
        reloadTime = _reloadTime;
        fireRate = _fireRate;
        currentClip = _clipSize;
        inverseFireRate = 1 / fireRate;
    }
    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            //Debug.Log ("clicked");
            PullTrigger ();
            cooldown = 0;
        } else if (Input.GetMouseButton (0) && IsAutomatic ()) {
            cooldown += Time.deltaTime;
            if (cooldown >= inverseFireRate) {
                cooldown -= inverseFireRate;
                PullTrigger ();
            }

        }
        if (Input.GetKey (KeyCode.R)) {
            Reload ();
        }
    }
    //Electric/ flamethrower would'nt need reload
    // Style ammo UI differently based on this bool
    public virtual bool UseReload () { return true; }

    public virtual bool HasAmmo () { return ammo > 0; }
    public virtual bool HasClipAmmo () { return currentClip > 0; }
    public virtual bool CanUse () { return !reloading && HasClipAmmo (); }

    //Ammo available but not in currentClip. Mainly for HUD
    public int ExtraAmmo () {
        return ammo - currentClip;
    }

    //This checks if a single click or held down..calls Fire();
    public void PullTrigger () {

        if (UseAmmo ()) {
            Fire ();
        }
    }

    public void Fire () {
        Debug.Log ("Bang");
        //var go = Instantiate (Bullet) as GameObject;
        /* go.transform.position = SpawnPoint.position;
        go.transform.forward = SpawnPoint.forward;
        go.GetComponent<Rigidbody> ().AddForce (go.transform.forward * 50, ForceMode.Impulse); */
    }

    public virtual bool UseAmmo () {

        if (currentClip <= 0 || reloading) {
            OnEmptyClip ();
            return false;
        }
        ammo--;
        currentClip--;
        OnAmmoChanged ();
        return true;
    }

    public void Reload () {
        if (!reloading) {
            if (UseReload ()) {
                reloading = true;
                StartCoroutine ("ReloadWeaponOverTime");
            } else {
                ApplyReload ();
            }
        }
    }
    void ApplyReload () {
        int ammoSpace = clipSize - currentClip;
        currentClip = Mathf.Min (clipSize, ExtraAmmo ());
        OnAmmoChanged ();
        reloading = false;
    }
    IEnumerator ReloadWeaponOverTime () {
        yield return new WaitForSeconds (reloadTime / 1000f);
        ApplyReload ();
    }

    //Change this to delegate so UI can listen and update HUD
    public void OnAmmoChanged () { }

    public void OnEmptyClip () { /*play sound*/ }

    public virtual bool IsAutomatic () {
        return true;
    }
    //if empty and pick up ammo, auto relaod
    public void PickUpAmmo (int amount) {
        ammo += amount;
        if (currentClip <= 0) {
            Reload ();
        }
        OnAmmoChanged ();
    }
}