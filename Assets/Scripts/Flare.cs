using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : Bullet {

    new float lifespan = 12f;
    public override void OnCollisionEnter (Collision collision) {
        Debug.Log (collision.collider);
        GetComponent<Rigidbody> ().isKinematic = true;
    }
}