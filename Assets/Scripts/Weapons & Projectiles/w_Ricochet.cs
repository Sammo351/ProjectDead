using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w_Ricochet : Weapon
{
    [Header("Significant")]
    public int bounces = 1;
    public override void Fire()
    {
        if (!isSilent)
        {
            AudioSource.PlayClipAtPoint(audioFire, transform.position);
        }
        GameObject go = GameObject.Instantiate(bullet, SpawnPoint.position, Quaternion.identity);
        Bullet bScript = go.GetComponent<Bullet>();
        bScript.damagePacket = new DamagePacket(Entity, (int)damage, damageType);
        bScript.owner = Entity;
        go.transform.forward = SpawnPoint.forward;
        if (bScript is BouncyBullet)
        {
            ((BouncyBullet)bScript).maxBounce = bounces;

        }
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * projectileSpeed, ForceMode.Impulse);
        if (!isSilent)
        {
            Senses.TriggerSoundAlert(transform.position, zombieTargetPriority);
        }
    }
}
