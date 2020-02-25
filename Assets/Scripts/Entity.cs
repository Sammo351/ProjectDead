using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs
{


    public class Entity : MonoBehaviour
    {

        int health;
        public int maxHealth;
        public float TimeFactor = 1f;


        public SimpleEntityEvent OnDeathEvent;


        public EntityEvent OnEntityKilled;

        public EntityEvent OnCollidedWith;
        public EntityTriggerEvent OnEnteredTrigger;

        public void OnCollisionEnter(Collision collision)
        {
        
            if (OnCollidedWith != null)
            {
                if (collision.gameObject.GetComponent<Entity>())
                    OnCollidedWith(GetComponent<Entity>(), collision.gameObject.GetComponent<Entity>());
            }
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (OnEnteredTrigger != null)
            {
                if (collider.gameObject.GetComponent<Trigger>())
                    OnEnteredTrigger(GetComponent<Entity>(), collider.gameObject.GetComponent<Trigger>());
            }
        }

        void Awake()
        {
            Initialise();
        }

        public virtual void Initialise()
        {
            Health = maxHealth;
        }

        public float deltaTime { get { return Time.deltaTime * TimeFactor; } }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                bool isAlive = health > 0;
                health = Mathf.Clamp(value, 0, maxHealth);
                if (isAlive && health <= 0 && OnDeathEvent != null)
                    OnDeathEvent(this);
            }
        }

        public virtual void OnDamage(int damageAmount,/* DamageType damageType,*/ Entity attacker)
        {
            Debug.Log(attacker);
            if (Health <= 0 && attacker != null)
            {
                if (OnEntityKilled != null)
                {
                    Debug.Log("Not even being damaged..");
                    OnEntityKilled(this, attacker);
                    Debug.Log("MEEEE DEAD");
                }
                else
                {
                    Debug.Log("Delegate is empty?");
                }
            }
        }
    }

    public delegate void SimpleEvent();
    public delegate void SimpleEntityEvent(Entity entity);
    public delegate void EntityEvent(Entity entity, Entity attacker);
    public delegate void EntityGameObjectEvent(Entity entity, GameObject gameObject);
    public delegate void EntityTriggerEvent(Entity entity, Trigger trigger);

}