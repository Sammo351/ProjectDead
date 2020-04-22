using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Entity, IDamageable, IShootable
{

    //public Transform Target;
    Animator animator;
    public float rangeForAttack = 2.7f;
    float attackDuration = 2f;
    public float baseSpeed = 3.5f;
    public float tickRate = 5f; //updates per second
    private float baseTickRate = 5f;
    float time = 0, baseTime = 0;
    private Transform _target = null;
    private bool attacking = false;
    AI ai;
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<NavMeshAgent>().speed = baseSpeed;
        ai = GetComponent<AI>();
        // GetComponent<BoxCollider> ().center = Vector3.up * 0.9f;
        // GetComponent<BoxCollider> ().size = Vector3.one * 0.1f;
    }

    public override void OnEntityDeath(Entity entity)
    {
        if (animator != null)
            animator.SetTrigger("death");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        baseTime += Time.deltaTime;
        if (time >= InverseTickRate)
        {
            time -= InverseTickRate;
            OnTick();
        }
        if (baseTime >= InverseBaseTickRate)
        {
            baseTime -= InverseBaseTickRate;
            OnBaseTick();
        }
    }
    public Transform Target
    {

        get { return _target; }
        set { _target = value; if (value != null) { OnTargetUpdate(); } }
    }
    float InverseTickRate
    {
        get { return 1 / (float)tickRate; }
    }
    float InverseBaseTickRate
    {
        get { return 1 / (float)baseTickRate; }
    }
    public virtual void OnTick()
    {

    }
    void OnBaseTick()
    {
        if (Target != null)
        {
            float distance = Vector3.Distance(Target.position, transform.position);
            if (distance <= rangeForAttack && !attacking)
            {
                attacking = true;
                OnZombieInRangeOfPlayer();
                StartCoroutine("EndAttack");
            }
        }
    }
    public void OnDamaged(DamagePacket packet)
    {
        Health -= packet.damage;
        //Debug.Log (packet.attacker);
        if (Health <= 0 && packet.attacker != null)
        {
            if (OnEntityKilled != null)
            {
                //Debug.Log ("Not even being damaged..");
                OnEntityKilled(this, packet.attacker);

                //Debug.Log ("MEEEE DEAD");
            }
            else
            {
                //Debug.Log ("Delegate is empty?");
            }
            XP.Give(xpValue);
            Destroy(gameObject);
        }
    }
    public void OnShot(GameObject projectile, DamagePacket packet)
    {
        OnDamaged(packet);
    }
    public virtual void OnTargetUpdate() { }
    public bool TargetInCorrectLayer()
    {
        Transform t = Target;
        if (t != null)
        {
            return t.gameObject.layer != LayerMask.NameToLayer("Player-Ignored");
        }
        return false;
    }
    public virtual void OnZombieInRangeOfPlayer()
    {

    }
    IEnumerable EndAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        attacking = false;
    }

}