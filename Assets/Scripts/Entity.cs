using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Entity : MonoBehaviour, ICanPickUpItem, IDamageable, IHealable {

    int health;
    public int maxHealth;
    public float TimeFactor = 1f;

    public SimpleEntityEvent OnDeathEvent;

    public EntityEvent OnEntityKilled;

    public EntityEvent OnCollidedWith;
    public EntityTriggerEvent OnEnteredTrigger;

    public void OnCollisionEnter (Collision collision) {

        if (OnCollidedWith != null) {
            if (collision.gameObject.GetComponent<Entity> ())
                OnCollidedWith (GetComponent<Entity> (), collision.gameObject.GetComponent<Entity> ());
        }
    }

    public void OnTriggerEnter (Collider collider) {
        if (OnEnteredTrigger != null) {
            if (collider.gameObject.GetComponent<Trigger> ())
                OnEnteredTrigger (GetComponent<Entity> (), collider.gameObject.GetComponent<Trigger> ());
        }
    }

    void Awake () {
        Initialise ();
    }

    public virtual void Initialise () {
        Health = maxHealth;
    }

    public float deltaTime { get { return Time.deltaTime * TimeFactor; } }

    public int Health {
        get {
            return health;
        }
        set {
            bool isAlive = health > 0;
            health = Mathf.Clamp (value, 0, maxHealth);
            if (isAlive && health <= 0 && OnDeathEvent != null)
                OnDeathEvent (this);
        }
    }

    public void OnDamaged (Entity attacker, int damageAmount, DamageType damageType) {
        health -= damageAmount;
        Debug.Log (attacker);
        if (Health <= 0 && attacker != null) {
            if (OnEntityKilled != null) {
                Debug.Log ("Not even being damaged..");
                OnEntityKilled (this, attacker);
                Debug.Log ("MEEEE DEAD");
            } else {
                Debug.Log ("Delegate is empty?");
            }
        }
    }
    public virtual void OnHeal (int healAmount) {
        Health += healAmount;
    }

    public bool OnPickUpItem (Drop drop) {
        Debug.Log ("Picked up " + drop.dropType.ToString ());
        switch (drop.dropType) {
            case DropType.Ammo:
                GetComponent<Inventory> ().GetPrimaryWeapon ().OnPickUpAmmo (drop.value);
                break;
            case DropType.Health:
                int before = Health;
                OnHeal (drop.value);
                return Health > before;
            case DropType.Perk:
                break;
        }
        return true;
    }

}

public delegate void SimpleEvent ();
public delegate void SimpleEntityEvent (Entity entity);
public delegate void EntityEvent (Entity entity, Entity attacker);
public delegate void EntityGameObjectEvent (Entity entity, GameObject gameObject);
public delegate void EntityTriggerEvent (Entity entity, Trigger trigger);