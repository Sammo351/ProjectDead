using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {
    public NavMeshAgent agent;
    public Transform Target;
    Animator animator;

    void Start () {
        animator = GetComponent<Animator> ();
        agent = GetComponent<NavMeshAgent> ();
        GetComponent<Entity> ().OnDeathEvent += EntityDied;
    }

    private void EntityDied (Entity entity) {
        animator.SetTrigger ("death");
    }

    // Update is called once per frame
    void Update () {
        if (GetComponent<Entity> ().Health > 0) {
            agent.SetDestination (Target.position);
        } else {
            //agent.isStopped = true;
            agent.enabled = false;
            GetComponent<BoxCollider> ().center = Vector3.up * 0.9f;
            GetComponent<BoxCollider> ().size = Vector3.one * 0.1f;
        }
    }
}