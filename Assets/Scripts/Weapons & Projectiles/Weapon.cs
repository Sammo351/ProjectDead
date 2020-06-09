using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Inventory))]
public class Weapon : MonoBehaviour
{

    [TitleGroup("Bullet settings")]
    public GameObject bullet;
    [TitleGroup("Bullet settings")]
    public Transform SpawnPoint;
    
    [TitleGroup("Ammo Settings")]
    public int clipSize, currentClip;
    [TitleGroup("Fire Settings")]
    public float fireRate; // how shots per second
    float inverseFireRate; // how many seconds per shot
    [TitleGroup("Reload Settings")]
    [ReadOnly] public bool reloading = false;
    [TitleGroup("Reload Settings")]
    public float reloadTime;
    float cooldown = 0; // how long until can fire next shot automatically 
    bool _isPrimary = false;
    [TitleGroup("Weapon Settings", Order =-1)]
    public string weaponName = "Pistol";
    [SerializeField]
    [TitleGroup("Weapon Settings")]
    private bool _automatic = true;
    [TitleGroup("Weapon Settings")]
    public bool isSilent = false;

    [TitleGroup("Damage Settings")]
    public float damage, damageModifier, piercingModifier;
    [TitleGroup("Damage Settings")]
    public DamageType damageType = DamageType.Bullet;

    [Title("Misc")]
    public AudioSource audioFire;
    float timeSinceLastFired = 100;

    bool isShooting = false;
    bool firedFirstShot = false;
    /* piercingModifier = does it pass through zombies? */

    Entity _entity;
    [ShowInInspector]
    Inventory _inventory;

    void Start()
    {
        _entity = GetComponent<Entity>();
        QuickSet();
        _inventory = gameObject.GetComponent<Inventory>();
        Debug.Log("Inventory set");
        if (_inventory == null)
        {
            _inventory = gameObject.AddComponent<Inventory>();
        }
    }
    //_reloadTime & _fireRate  = /per second
    public void QuickSet(int _ammo = 90, int _clipSize = 30, float _reloadTime = 2.5f, float _fireRate = 5)
    {
        /*   ammo = _ammo;
          maxAmmo = _ammo; */
        clipSize = _clipSize;
        reloadTime = _reloadTime;
        fireRate = (float)_fireRate;
        currentClip = Mathf.Min(_clipSize, Ammo);
        inverseFireRate = 1 / fireRate;
    }
    void Update()
    {
        timeSinceLastFired += Time.deltaTime;

        if (IsPrimary && isShooting)
        {
            Shoot();
        }

    }
    public void ShootStart()
    {
        if (IsPrimary) { isShooting = true; }
    }
    public void StopShoot()
    {
        isShooting = false;
        firedFirstShot = false;
    }
    public void Shoot()
    {

        if (!firedFirstShot)
        {
            if (timeSinceLastFired >= inverseFireRate)
            {
                PullTrigger();
                cooldown = 0;
                firedFirstShot = true;
            }

        }
        else if (IsAutomatic)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= inverseFireRate)
            {
                cooldown -= inverseFireRate;
                PullTrigger();
            }

        }
    }
    //Is this weapon in players hand
    public virtual bool IsPrimary
    {
        get { return _isPrimary; }
        set { _isPrimary = value; }
    }
    //Electric/ flamethrower would'nt need reload
    // Style ammo UI differently based on this bool
    public virtual bool UseReload() { return true; }

    public virtual bool HasAmmo() { return Ammo > 0; }
    public virtual bool HasClipAmmo() { return currentClip > 0; }
    public virtual bool CanUse() { return !reloading && HasClipAmmo(); }
    public float zombieTargetPriority = 3;
    //Ammo available but not in currentClip. Mainly for HUD
    public int ExtraAmmo()
    {
        return Ammo - currentClip;
    }

    //This checks if a single click or held down..calls Fire();
    public void PullTrigger()
    {

        if (UseAmmo())
        {
            timeSinceLastFired = 0;
            Fire();
        }
    }

    
    public void Fire()
    {
        var go = Instantiate(bullet) as GameObject;
        Bullet _bulletObj = go.GetComponent<Bullet>();
        _bulletObj.damagePacket = new DamagePacket(_entity, (int)damage, damageType);
        _bulletObj.owner = _entity;
        go.transform.position = SpawnPoint.position;
        go.transform.forward = SpawnPoint.forward;
        go.GetComponent<Rigidbody>()?.AddForce(go.transform.forward * _bulletObj.speed, ForceMode.Impulse);

        if (!isSilent)
        {
            Senses.TriggerSoundAlert(transform.position, zombieTargetPriority);
            audioFire.Play();
        }
    }


    public int Ammo
    {
        get { return _inventory ? _inventory.Ammo : 0; }
        set { _inventory.Ammo = value; }
    }



    public virtual bool UseAmmo()
    {

        if (currentClip <= 0 || reloading)
        {
            OnEmptyClip();
            return false;
        }
        currentClip--;
        OnAmmoChanged();
        return true;
    }

    public void Reload()
    {
        if (!reloading && HasAmmo())
        {
            if (UseReload())
            {
                reloading = true;
                StartCoroutine("ReloadWeaponOverTime");
            }
            else
            {
                ApplyReload();
            }
        }
    }
    void ApplyReload()
    {

        reloading = false;
        int ammoSpace = clipSize - currentClip;
        int min = Mathf.Min(ammoSpace, Ammo);
        currentClip += min;
        Ammo -= min;
        OnAmmoChanged();
    }
    IEnumerator ReloadWeaponOverTime()
    {
        yield return new WaitForSeconds(reloadTime);
        ApplyReload();
    }

    //Change this to delegate so UI can listen and update HUD
    public void OnAmmoChanged() { }

    public void OnEmptyClip() { /*play sound*/ }

    public virtual bool IsAutomatic
    {
        get { return _automatic; }
        set { _automatic = value; }
    }
    //if empty and pick up ammo, auto relaod
    public void OnPickUpAmmo(int amount)
    {
        Ammo += amount;
        if (currentClip <= 0)
        {
            Reload();
        }
        OnAmmoChanged();
    }
}