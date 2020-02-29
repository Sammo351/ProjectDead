using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnTriggerEnter (Collider collision) {
        var en = collision.gameObject.GetComponent<Entity> ();
        if (en) {
            en.Health -= 1;
            Destroy (this.gameObject);
        }
    }
}