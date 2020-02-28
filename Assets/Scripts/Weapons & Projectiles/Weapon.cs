using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject Bullet;
    public Transform SpawnPoint;
    public float ammo, maxAmmo, clipSize, reloadTime, fireRate;
    public bool automatic = false;

    void Start () {

    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            var go = Instantiate (Bullet) as GameObject;
            go.transform.position = SpawnPoint.position;
            go.transform.forward = SpawnPoint.forward;
            go.GetComponent<Rigidbody> ().AddForce (go.transform.forward * 50, ForceMode.Impulse);

        }
    }
    //Electric/ flamethrower would'nt need reload
    // Style ammo UI differently based on this bool
    public virtual bool UseReload () { return true; }
    public virtual bool HasAmmo () { return ammo > 0; }

    //This checks if a single click or held down..calls Fire();
    public void PullTrigger () {

    }
    public void Fire () {

    }
}