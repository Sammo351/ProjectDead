using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour {
    [SerializeField]
    private int _ammo = 90;
    void Start () {
        LoadWeapons ();
    }
    void Update () {
        var d = Input.GetAxis ("Mouse ScrollWheel");
        if (Mathf.Abs (d) > 0f) {
            CycleWeapon (d < 0);
        }

    }
    // Get all weapons and set first one to primary
    void LoadWeapons () {
        Weapon[] weapons = GetWeapons ();
        if (weapons.Length > 0) {
            SetPrimaryWeapon (weapons[0]);
        }
    }

    Weapon[] GetWeapons () {
        return gameObject.GetComponents<Weapon> ();
    }
    void CycleWeapon (bool forward = true) {
        int currentIndex = 0;
        Weapon[] weps = GetWeapons ();
        for (int i = 0; i < weps.Length; i++) {
            if (weps[i].IsPrimary) {
                weps[i].StopCoroutine ("ReloadWeaponOverTime");
                weps[i].reloading = false;
                currentIndex = i;
                break;
            }
        }
        currentIndex += 1 * (forward?1: -1);
        currentIndex = (int) Mathf.Repeat ((float) currentIndex, (float) weps.Length);
        SetPrimaryWeapon (weps[currentIndex]);
    }

    void SetPrimaryWeapon (Weapon weapon) {
        Weapon[] weps = GetWeapons ();
        for (int i = 0; i < weps.Length; i++) {
            weps[i].IsPrimary = false;
        }
        weapon.IsPrimary = true;
    }
    public Weapon GetPrimaryWeapon () {
        Weapon[] weps = GetWeapons ();
        for (int i = 0; i < weps.Length; i++) {
            if (weps[i].IsPrimary) { return weps[i]; }
        }
        return null;
    }
    public bool OnPickUp (Drop drop) {
        switch (drop.dropType) {
            case DropType.Ammo:
                GetPrimaryWeapon ().OnPickUpAmmo (drop.value);
                return true;
        }
        return false;
    }
    public int Ammo { get { return _ammo; } set { _ammo = value; } }

}
public enum AmmoType { Energy }
/* 
    Primary weapon = inhand 
    Secondary weapon = in backpack

 */