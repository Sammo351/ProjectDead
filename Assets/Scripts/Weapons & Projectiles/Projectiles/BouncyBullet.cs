using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : Bullet
{


    public int maxBounce = 3;



    public override void OnCollisionEnter(Collision collision)
    {
        //Debug.Log ("Bullet collided " + collision.collider.gameObject);
        var en = collision.collider.gameObject.GetComponents<IShootable>();


        if (en != null)
        {
            //Debug.Log ("Target is shootable");
            for (int i = 0; i < en.Length; i++)
            {
                en[i].OnShot(gameObject, damagePacket);
                //Destroy(collision.collider.gameObject);

            }
        }

        maxBounce--;
        if (maxBounce < 0)
        {
            trail.transform.parent = null;
            trail.autodestruct = true;
            trail.time *= 0.5f;
            trail = null;
            Destroy(this.gameObject);
            return;
        }
        if (collision.collider.gameObject.GetComponent<Zombie>() != null)
        {
            Debug.Log("Searching");
            GameObject target = NextTarget(collision.collider.gameObject);
            if (target != null)
            {
                Debug.Log("Found: " + target);
                Vector3 dir = target.transform.position - transform.position;
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                dir = dir.SetY(0).normalized;
                gameObject.GetComponent<Rigidbody>().AddForce(dir * speed, ForceMode.Impulse);
            }
        }
    }
    GameObject NextTarget(GameObject current, float radius = 30)
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemies"), QueryTriggerInteraction.UseGlobal);
        if (cols.Length <= 0)
        {
            Destroy(gameObject);
            return null;
        }
        for (int i = 0; i < cols.Length; i++)
        {
            Collider col = cols[i];
            GameObject g = col.gameObject;
            if (g != gameObject && g != current)
            {
                Debug.Log("FIX THE LINE OF SIGHT");
                if (true || Ext.LineOfSight(gameObject, g))
                {
                    return g;
                }
            }
        }
        Destroy(gameObject);
        return null;
    }

}