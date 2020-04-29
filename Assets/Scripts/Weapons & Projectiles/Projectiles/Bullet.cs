using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Material trailMaterial;
    public float lifespan = 8f;
    public Entity owner;
    public DamagePacket damagePacket;
    [ReadOnly] public float life = 0;
    public float speed = 10;

    public TrailRenderer trail;

    private void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        trail.material = new Material(trailMaterial);
        PlayerController pc = owner.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            trail.material.SetColor("_EmissionColor", pc.PlayerColor);
            GetComponent<Light>().color = pc.PlayerColor;
        }

    }

    public void Update()
    {
        life += Time.deltaTime;
        if (life >= lifespan)
        {
            Destroy(gameObject);
        }
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
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

        Destroy(this.gameObject);
    }

}