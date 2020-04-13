using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Inventory))]
public class Weapon : MonoBehaviour {
    public GameObject bullet;
    public Transform SpawnPoint;
    public DamageType damageType = DamageType.Bullet;
    public int clipSize, currentClip;
    public float fireRate, reloadTime; // how shots per second
    float inverseFireRate; // how many seconds per shot
    [ReadOnly] public bool reloading = false;
    float cooldown = 0; // how long until can fire next shot automatically 
    bool _isPrimary = false;
    public string weaponName = "Pistol";
    [SerializeField]
    private bool _automatic = true;
    public bool isSilent = false;
    public float damage, damageModifier, piercingModifier;
    public AudioSource audioFire;

    /* piercingModifier = does it pass through zombies? */

    void Start () {
        QuickSet ();
        if (!GetComponent<Inventory> ()) {
            gameObject.AddComponent<Inventory> ();
        }
    }
    //_reloadTime & _fireRate  = /per second
    public void QuickSet (int _ammo = 90, int _clipSize = 30, float _reloadTime = 2.5f, float _fireRate = 5) {
        /*   ammo = _ammo;
          maxAmmo = _ammo; */
        clipSize = _clipSize;
        reloadTime = _reloadTime;
        fireRate = (float) _fireRate;
        currentClip = Mathf.Min (_clipSize, Ammo);
        inverseFireRate = 1 / fireRate;
    }
    void Update () {

        if (IsPrimary) {
            OnInput ();
        }

    }
    public void OnInput () {
        if (Input.GetMouseButtonDown (0)) {
            //Debug.Log ("clicked");
            PullTrigger ();
            cooldown = 0;
        } else if (Input.GetMouseButton (0) && IsAutomatic) {
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
    //Is this weapon in players hand
    public virtual bool IsPrimary {
        get { return _isPrimary; }
        set { _isPrimary = value; }
    }
    //Electric/ flamethrower would'nt need reload
    // Style ammo UI differently based on this bool
    public virtual bool UseReload () { return true; }

    public virtual bool HasAmmo () { return Ammo > 0; }
    public virtual bool HasClipAmmo () { return currentClip > 0; }
    public virtual bool CanUse () { return !reloading && HasClipAmmo (); }

    //Ammo available but not in currentClip. Mainly for HUD
    public int ExtraAmmo () {
        return Ammo - currentClip;
    }

    //This checks if a single click or held down..calls Fire();
    public void PullTrigger () {

        if (UseAmmo ()) {
            Fire ();
        }
    }

    public void Fire () {
        //Debug.Log ("Bang");
        audioFire.Play();
        var go = Instantiate (bullet) as GameObject;
        go.GetComponent<Bullet> ().damagePacket = new DamagePacket (GetComponent<Entity> (), (int) damage, damageType);
        go.GetComponent<Bullet> ().owner = GetComponent<Entity> ();
        go.transform.position = SpawnPoint.position;
        go.transform.forward = SpawnPoint.forward;
        go.GetComponent<Rigidbody> ().AddForce (go.transform.forward * 50, ForceMode.Impulse);
        if (!isSilent) {
            Senses.TriggerSoundAlert (transform.position);
        }
    }
    public int Ammo {
        get { return GetComponent<Inventory> ().Ammo; }
        set { GetComponent<Inventory> ().Ammo = value; }
    }
    public virtual bool UseAmmo () {

        if (currentClip <= 0 || reloading) {
            OnEmptyClip ();
            return false;
        }
        currentClip--;
        OnAmmoChanged ();
        return true;
    }

    public void Reload () {
        if (!reloading && HasAmmo ()) {
            if (UseReload ()) {
                reloading = true;
                StartCoroutine ("ReloadWeaponOverTime");
            } else {
                ApplyReload ();
            }
        }
    }
    void ApplyReload () {

        reloading = false;
        int ammoSpace = clipSize - currentClip;
        int min = Mathf.Min (ammoSpace, Ammo);
        currentClip += min;
        Ammo -= min;
        OnAmmoChanged ();
    }
    IEnumerator ReloadWeaponOverTime () {
        yield return new WaitForSeconds (reloadTime);
        ApplyReload ();
    }

    //Change this to delegate so UI can listen and update HUD
    public void OnAmmoChanged () { }

    public void OnEmptyClip () { /*play sound*/ }

    public virtual bool IsAutomatic {
        get { return _automatic; }
        set { _automatic = value; }
    }
    //if empty and pick up ammo, auto relaod
    public void OnPickUpAmmo (int amount) {
        Ammo += amount;
        if (currentClip <= 0) {
            Reload ();
        }
        OnAmmoChanged ();
    }
}