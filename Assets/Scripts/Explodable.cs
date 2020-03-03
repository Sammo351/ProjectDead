using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Explodable : MonoBehaviour, IDamageable, IShootable {
    // Start is called before the first frame update
    public float damageRadius = 5f, maxDamage = 15, minDamage = 4, force = 15f;
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
                id.OnDamaged (new DamagePacket (null, Mathf.RoundToInt (damage), DamageType.Blast));

            }
            if (g != null && g.GetComponent<Rigidbody> () != null) {
                g.GetComponent<Rigidbody> ().AddExplosionForce (force, transform.position, damageRadius, 1.2f, ForceMode.Impulse);
            }
        }
        Destroy (gameObject);

    }
    public void OnDamaged (DamagePacket packet) {
        health -= packet.damage;
        if (health <= 0) {
            Explode ();
        }
    }

    public void OnShot (GameObject projectile, DamagePacket packet) {
        OnDamaged (packet);
        Destroy (gameObject);
    }

    [ContextMenu ("Explode")]
    public void Damage () {
        OnDamaged (new DamagePacket (null, 10, DamageType.Blast));
    }
    void OnDrawGizmos () { Handles.DrawWireDisc (transform.position, Vector3.up, damageRadius); }
}