using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Damage {

    public static void DamageEntity (Entity from, IDamageable obj, DamageType type, int val) {
        if (obj != null) {
            obj.OnDamaged (from, val, type);
        }
        //send to UI for effects (+10)
    }
    public static void HealEntity (IHealable obj, int val) {
        if (obj != null) {
            obj.OnHeal (val);
        }
    }

}
public enum DamageType { Blast, Bullet, Fire }