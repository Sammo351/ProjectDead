using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerZombie : Zombie
{

    public override void OnTick()
    {

    }
    public override void OnZombieInRangeOfPlayer()
    {
        if (TargetInCorrectLayer())
        {
            WorldHelper.AddExplosion(transform.position, 3f, 0.4f, 5, this);
            Destroy(gameObject);
        }
    }
}