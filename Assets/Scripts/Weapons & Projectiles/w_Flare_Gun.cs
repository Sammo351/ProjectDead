using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w_Flare_Gun : Weapon
{
    [Header("Significant")]
    public float litDuration = 12;
    public override void OnBulletCreated(Bullet bullet)
    {
        if (bullet is FlareBullet)
        {
            ((FlareBullet)bullet).lifespan = litDuration;
        }
    }
}
