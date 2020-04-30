using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int _ammoPrimary = 90, _ammoSpecial = 4, _ammoThrowable = 1;
    [SerializeField]
    private int _ammoPrimaryMax = 90, _ammoSpecialMax = 4, _ammoThrowableMax = 1;
    [SerializeField]
    private int _xp;
    int weaponIndex = 0;// 0= primary 1 = special
    [SerializeField]
    Weapon primaryWeapon, specialWeapon;
    //throwableWeapon;
    void Start()
    {
        GetWeapons();
        SetActiveWeapon(weaponIndex);

    }
    void Update()
    {

    }
    #region Weapon Functions
    /*   void LoadWeapons()
      {
          Debug.Log("LOADED!!!!!");
          Weapon[] weapons = GetWeapons();
          if (weapons.Length > 0)
          {
              SetActiveWeapon(primaryWeapon);
          }
      } */

    Weapon[] GetWeapons()
    {
        Weapon[] weps = gameObject.GetComponentsInChildren<Weapon>();
        bool gotPrimary = false;
        bool gotSpecial = false;
        for (int i = 0; i < weps.Length; i++)
        {
            Weapon wep = weps[i];
            if (wep.weaponType == WeaponType.Primary && !gotPrimary && wep.enabled)
            {
                primaryWeapon = wep;
                gotPrimary = true;
            }
            else if (wep.weaponType == WeaponType.Special && !gotSpecial && wep.enabled)
            {
                specialWeapon = wep;
                gotSpecial = true;
            }
        }
        return weps;
    }
    public void CycleWeapon()
    {
        weaponIndex = weaponIndex == 0 ? 1 : 0;
        SetActiveWeapon(weaponIndex);
    }
    Weapon GetWeaponFromIndex()
    {
        GetWeapons();
        return weaponIndex == 0 ? primaryWeapon : specialWeapon;
    }
    void SetActiveWeapon(int index)
    {

        if (index == 0)
        {
            weaponIndex = index;
            primaryWeapon.IsActive = true;
            specialWeapon.IsActive = false;
        }
        else if (index == 1)
        {
            weaponIndex = index;
            primaryWeapon.IsActive = false;
            specialWeapon.IsActive = true;
        }

        //HolsterWeapon(GetHolsteredWeapon());

    }
    public Weapon GetHolsteredWeapon()
    {
        return weaponIndex == 0 ? specialWeapon : primaryWeapon;
    }
    public Weapon GetActiveWeapon()
    {
        return GetWeaponFromIndex();
    }
    /*  void HolsterWeapon(Weapon weapon)
     {
         weapon.gameObject.transform.parent = Player().transform.Find("Reserve_Weapon_Holder");
     } */
    public bool OnPickUp(Drop drop)
    {
        switch (drop.dropType)
        {
            case DropType.Ammo:
                GetActiveWeapon().OnPickUpAmmo(drop.value);
                return true;
        }
        return false;
    }
    #endregion
    GameObject Player()
    {
        return GetComponentInParent<PlayerController>().gameObject;
    }
    /* --------------------------------------------------------AMMO FUNCTIONS------------------------------------------------------ */
    #region Ammo Functions
    public int Ammo(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Primary: return _ammoPrimary;
            case WeaponType.Special: return _ammoSpecial;
            case WeaponType.Throwable: return _ammoThrowable;
        }
        return 0;
    }
    /* This will be affected by upgrades later */
    public int AmmoMax(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Primary: return _ammoPrimaryMax;
            case WeaponType.Special: return _ammoSpecialMax;
            case WeaponType.Throwable: return _ammoThrowableMax;
        }
        return 0;
    }
    public void SetAmmo(WeaponType weaponType, int val)
    {
        switch (weaponType)
        {
            case WeaponType.Primary: _ammoPrimary = Mathf.Clamp(val, 0, AmmoMax(WeaponType.Primary)); break;
            case WeaponType.Special: _ammoSpecial = Mathf.Clamp(val, 0, AmmoMax(WeaponType.Special)); break;
            case WeaponType.Throwable: _ammoThrowable = Mathf.Clamp(val, 0, AmmoMax(WeaponType.Throwable)); break;
        }
    }
    /* Retuns the amount of ammo that couldnt be kept ( so drop it) */
    public int AddAmmo(WeaponType weaponType, int val)
    {
        if (val <= 0) { return 0; }
        int before = Ammo(weaponType);
        int after = before + val;
        int rem = after - AmmoMax(weaponType);
        SetAmmo(weaponType, after);
        return rem > 0 ? rem : 0;
    }
    /* Returns amount of ammo taken from inventory */
    public int RemoveAmmo(WeaponType weaponType, int val)
    {
        if (val <= 0) { return 0; }
        int before = Ammo(weaponType);
        int taken = Mathf.Min(before, val);
        SetAmmo(weaponType, before - taken);
        return taken;
    }
    #endregion
    public int XP
    {
        get { return _xp; }
        set { _xp = value; _xp = Mathf.Max(_xp, 0); }
    }
    /*   void SaveInventory()
      {
          string fileName = GetComponent<PlayerController>().playerName + "_inv";
          string OutputPath = Application.persistentDataPath + @"\" + fileName + ".json";
          StreamWriter file = File.CreateText(OutputPath);
          file.WriteLine(JsonUtility.ToJson(this));
          file.Close();
      } */

}

