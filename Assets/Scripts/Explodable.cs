using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Explodable : MonoBehaviour, IDamageable {
    // Start is called before the first frame update
    public float damageRadius = 5f, maxDamage = 15, minDamage = 4;
    public int health = 0;
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
    void Explode () {
        Collider[] cols = Physics.OverlapSphere (transform.position, damageRadius);
        Debug.Log (cols);
        Destroy (gameObject.GetComponent<Rigidbody> ());
        for (int i = 0; i < cols.Length; i++) {
            GameObject g = cols[i].gameObject;
            if (g == gameObject) {
                continue;
            }
            IDamageable id = g.GetComponent<IDamageable> ();
            if (id != null) {
                float distance = Vector3.Distance (g.transform.position, transform.position);
                float p = Mathf.Clamp01 (distance / damageRadius);
                float damage = p.Map (0, 1, maxDamage, minDamage);
                id.OnDamaged (null, Mathf.RoundToInt (damage), DamageType.Blast);

            }
            if (g != null && g.GetComponent<Rigidbody> () != null) {
                g.GetComponent<Rigidbody> ().AddExplosionForce (15f, transform.position, damageRadius, 1.2f, ForceMode.Impulse);
            }
        }
        Destroy (gameObject);

    }
    public void OnDamaged (Entity from, int value, DamageType type) {
        health -= value;
        if (health <= 0) {
            Explode ();
        }
    }
    void OnCollisionEnter (Collision collision) {
        Collider col = collision.collider;
        Debug.Log (col.gameObject);
        Bullet b = col.GetComponent<Bullet> ();
        if (b != null) {
            OnDamaged (b.owner, 5, DamageType.Bullet);
            Destroy (col.gameObject);
        }
    }

    [ContextMenu ("Explode")]
    public void Damage () {
        OnDamaged (null, 10, DamageType.Blast);
    }
    void OnDrawGizmos () { Handles.DrawWireDisc (transform.position, Vector3.up, damageRadius); }
}