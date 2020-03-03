using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float lifespan = 2f;
    public Entity owner;
    public DamagePacket damagePacket;
    [ReadOnly, SerializeField] float life = 0;
    void Update () {
        life += Time.deltaTime;
        if (life >= lifespan) {
            Destroy (gameObject);
        }
    }
    public virtual void OnCollisionEnter (Collision collision) {
        Debug.Log ("Bullet collided " + collision.collider.gameObject);
        var en = collision.collider.gameObject.GetComponent<IShootable> ();

        if (collision.collider.gameObject.GetComponent<IShootable> () != null) {
            Debug.Log ("Target is shootable");
            collision.collider.gameObject.GetComponent<IShootable> ().OnShot (gameObject, damagePacket);
            Destroy (this.gameObject);
        }
    }

}