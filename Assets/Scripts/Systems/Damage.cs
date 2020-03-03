using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Damage {

    public static void DamageEntity (Entity from, IDamageable obj, DamageType type, int val) {
        if (obj != null) {
            obj.OnDamaged (new DamagePacket (from, val, type));
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
public struct DamagePacket {
    public int damage;
    public DamageType type;
    public Entity attacker;
    public DamagePacket (Entity _attacker, int _damage, DamageType _type) {
        attacker = _attacker;
        type = _type;
        damage = _damage;
    }
}