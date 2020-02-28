using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Damage {

    public static void DamageEntity (Entity from, Entity to, DamageType type, int val) {
        if (to != null && to.CanDamage ()) {
            to.OnDamage (from, val, type);
        }
        //send to UI for effects (+10)
    }
    public static void HealEntity (Entity entity, int val) {
        if (entity != null && entity.CanHeal ()) {
            entity.OnHeal (val);
        }
    }
}
public enum DamageType { Bullet, Fire }