using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Damage 
{
   
    public static void DamageEntity(Entity from, Entity to, DamageType type, int val )
    {
        to.OnDamage(val,type,from);
        //send to UI for effects (+10)
    }
    public static void HealEntity(Entity entity, int val)
    {
        entity.OnHeal(val);
    }
}
public enum DamageType {Bullet,Fire};
