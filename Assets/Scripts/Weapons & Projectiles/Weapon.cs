using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    [Header("Base")]
    [SerializeField]
    public WeaponType weaponType = WeaponType.Primary;
    public GameObject bullet;
    public Transform SpawnPoint;
    public DamageType damageType = DamageType.Bullet;
    public int clipSize, currentClip;
    public float fireRate, reloadTime; // how shots per second
    public float projectileSpeed = 10;
    float inverseFireRate; // how many seconds per shot
    [ReadOnly] public bool reloading = false;
    float cooldown = 0; // how long until can fire next shot automatically 
    bool _isActive = false;
    public string weaponName = "Pistol";
    [SerializeField]
    private bool _automatic = true;
    public bool isSilent = false;
    public float damage, damageModifier, piercingModifier;
    public AudioClip audioFire;
    float timeSinceLastFired = 100;

    bool isShooting = false;
    bool firedFirstShot = false;
    /* piercingModifier = does it pass through zombies? */

    void Start()
    {
        currentClip = Mathf.Min(clipSize, Ammo);
        currentClip = Mathf.Min(currentClip, Inv.AmmoMax(weaponType));
        inverseFireRate = 1 / fireRate;
    }
    //_reloadTime & _fireRate  = /per second
    public void QuickSet(int _clipSize = 30, float _reloadTime = 2.5f, float _fireRate = 5)
    {

        clipSize = _clipSize;
        reloadTime = _reloadTime;
        fireRate = (float)_fireRate;
        currentClip = Mathf.Min(_clipSize, Ammo);
        inverseFireRate = 1 / fireRate;
    }
    void Update()
    {


        timeSinceLastFired += Time.deltaTime;

        if (IsActive && isShooting)
        {
            Shoot();
        }

    }
    public void ShootStart()
    {
        if (IsActive) { isShooting = true; }
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
    public Inventory Inv
    {
        get { return GetComponentInParent<Inventory>(); }
    }
    public Entity Entity
    {
        get { return GetComponentInParent<Entity>(); }
    }
    //Is this weapon in players hand
    public virtual bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
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

    public virtual void Fire()
    {
        if (!isSilent)
        {
            AudioSource.PlayClipAtPoint(audioFire, transform.position);
        }
        GameObject go = GameObject.Instantiate(bullet, SpawnPoint.position, Quaternion.identity);
        go.GetComponent<Bullet>().damagePacket = new DamagePacket(Entity, (int)damage, damageType);
        go.GetComponent<Bullet>().owner = Entity;
        go.GetComponent<Bullet>().speed = projectileSpeed;
        OnBulletCreated(go.GetComponent<Bullet>());
        go.transform.forward = SpawnPoint.forward;
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * projectileSpeed, ForceMode.Impulse);
        if (!isSilent)
        {
            Senses.TriggerSoundAlert(transform.position, zombieTargetPriority);
        }
    }
    public virtual void OnBulletCreated(Bullet bullet)
    {
        // Easy way to apply your weapons abilities to the bullet (See w_Flare_Gun for an example)
    }
    public int Ammo
    {
        get { return Inv.Ammo(weaponType); }
        set { Inv.SetAmmo(weaponType, value); }
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
        Inv.RemoveAmmo(weaponType, min);
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