using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeapon : MonoBehaviour {

    public UpgradeWeaponType type = UpgradeWeaponType.Damage;
    public float value = 1;
}
public enum UpgradeWeaponType { Damage, FireRate, Reload, ClipSize, Piercing }