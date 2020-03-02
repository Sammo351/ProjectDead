using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float lifespan = 2f;
    public Entity owner;
    [ReadOnly, SerializeField] float life = 0;
    void Update () {
        life += Time.deltaTime;
        if (life >= lifespan) {
            Destroy (gameObject);
        }
    }
    public virtual void OnCollisionEnter (Collision collision) {
        var en = collision.collider.gameObject.GetComponent<Entity> ();
        if (en) {
            en.Health -= 1;
            Destroy (this.gameObject);
        }
    }

}