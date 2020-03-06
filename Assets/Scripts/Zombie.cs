using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Entity, IDamageable, IShootable {

    //public Transform Target;
    Animator animator;

    void Start () {
        animator = GetComponent<Animator> ();

        GetComponent<Entity> ().OnDeathEvent += EntityDied;
        // GetComponent<BoxCollider> ().center = Vector3.up * 0.9f;
        // GetComponent<BoxCollider> ().size = Vector3.one * 0.1f;
    }

    private void EntityDied (Entity entity) {
        animator.SetTrigger ("death");
    }

    // Update is called once per frame
    void Update () {

    }
    public void OnDamaged (DamagePacket packet) {
        Health -= packet.damage;
        //Debug.Log (packet.attacker);
        if (Health <= 0 && packet.attacker != null) {
            if (OnEntityKilled != null) {
                //Debug.Log ("Not even being damaged..");
                OnEntityKilled (this, packet.attacker);

                //Debug.Log ("MEEEE DEAD");
            } else {
                //Debug.Log ("Delegate is empty?");
            }
            XP.Give (xpValue);
            Destroy (gameObject);
        }
    }
    public void OnShot (GameObject projectile, DamagePacket packet) {
        OnDamaged (packet);
    }

}