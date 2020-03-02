using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent (typeof (SphereCollider))]
public class Drop : MonoBehaviour {
    public DropType dropType = DropType.Ammo;
    public int value = 10;
    public float pickupRadius = 0.6f;
    void Awake () {
        GetComponent<SphereCollider> ().radius = pickupRadius / transform.localScale.x;
        GetComponent<SphereCollider> ().isTrigger = true;
    }

    void Start () {

    }

    void ApplyCorrectColor () {
        GetComponent<Renderer> ().material.color = DropTypeColor (dropType);
    }
    void Update () {

    }
    void OnValidate () {
        ApplyCorrectColor ();
    }
    void OnDrawGizmos () {

        Handles.DrawWireDisc (transform.position, Vector3.up, pickupRadius);
    }
    void OnTriggerEnter (Collider col) {
        if (col == null) {
            return;
        }
        ICanPickUpItem ic = col.gameObject.GetComponent<ICanPickUpItem> ();
        if (ic != null) {
            if (ic.OnPickUpItem (this)) {
                GameObject.Destroy (gameObject);
            }
        }
    }
    public static Color DropTypeColor (DropType type) {
        switch (type) {
            case DropType.Ammo:
                return new Color (0 / 255f, 191 / 255f, 255 / 255f); //blue
            case DropType.Health:
                return Color.green;
            case DropType.Perk:
                return new Color (153 / 255f, 50 / 255f, 204 / 255f); //purple
        }
        return Color.grey;
    }

}

public enum DropType { Ammo, Health, Perk, XP }