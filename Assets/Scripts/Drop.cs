using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent (typeof (SphereCollider))]
public class Drop : MonoBehaviour {
    public DropType dropType = DropType.Ammo;
    public int value = 10;
    public float pickupRadius = 1.7f;
    void Start () {
        GetComponent<SphereCollider> ().radius = pickupRadius;
    }
    void Update () {

    }
    void OnDrawGizmos () {

        Handles.DrawWireDisc (transform.position, Vector3.up, pickupRadius);
    }
    void OnCollisionEnter (Collision col) {
        if (col == null) {
            return;
        }
        Entity ent = col.gameObject.GetComponent<Entity> ();
        if (ent != null) {
            if (ent.OnPickUpDrop (this)) {
                GameObject.Destroy (gameObject);
            }
        }
    }

}
public enum DropType { Ammo, Health, Perk }