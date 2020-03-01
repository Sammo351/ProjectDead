using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour {

    void Start () {

    }
    void Update () {

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

    void SetPrimaryWeapon (Weapon weapon) {
        Weapon[] weps = GetWeapons ();
        for (int i = 0; i < weps.Length; i++) {
            weps[i].IsPrimary = false;
        }
        weapon.IsPrimary = true;
    }

}
public enum AmmoType { Energy }
/* 
    Primary weapon = inhand 
    Secondary weapon = in backpack

 */