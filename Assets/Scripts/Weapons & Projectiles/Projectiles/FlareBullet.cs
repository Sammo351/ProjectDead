using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareBullet : Bullet
{


    public void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        trail.material = new Material(trailMaterial);
        PlayerController pc = owner.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            trail.material.SetColor("_EmissionColor", pc.PlayerColor);
        }

    }
    public new void Update()
    {
        base.Update();
        float lifeRemain = lifespan - life;
        if (lifeRemain <= 1.5f)
        {
            GetComponentInChildren<Aura2API.LightFlicker>().maxFactor = 0;
            GetComponentInChildren<Aura2API.LightFlicker>().minFactor = 0;
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Debug.Log ("Bullet collided " + collision.collider.gameObject);
        var en = collision.collider.gameObject.GetComponents<IShootable>();

        if (en != null)
        {
            //Debug.Log ("Target is shootable");
            for (int i = 0; i < en.Length; i++)
            {
                en[i].OnShot(gameObject, damagePacket);
            }

        }


        trail.transform.parent = null;
        trail.autodestruct = true;
        trail.time *= 0.5f;
        trail = null;


    }

}