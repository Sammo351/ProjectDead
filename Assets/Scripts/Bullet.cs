using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float lifespan = 2f;
    public Entity owner;
    public DamagePacket damagePacket;
    [ReadOnly, SerializeField] float life = 0;

    private TrailRenderer trail;

    private void Start()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    void Update () {
        life += Time.deltaTime;
        if (life >= lifespan) {
            Destroy (gameObject);
        }
    }
    public virtual void OnCollisionEnter (Collision collision) {
        //Debug.Log ("Bullet collided " + collision.collider.gameObject);
        var en = collision.collider.gameObject.GetComponents<IShootable> ();

        if (en != null) {
            //Debug.Log ("Target is shootable");
            for (int i = 0; i < en.Length; i++)
            {
                en[i].OnShot(gameObject, damagePacket);
            }
          
        }

        trail.transform.parent = null;
        trail.autodestruct = true;
        trail.time = 0.1f;
        Destroy(this.gameObject);
    }

}