using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterZombie : Zombie {
    // what tier is this zombie? depth 2 will die and produce depth 1 zombies
    public int tier = 2;
    [Range (2, 5)]
    public int spawnAmount = 2;
    public bool spawnlingsAutoTarget = true;
    /*  public override void Start () {
         base.Start ();
         GetComponent<AI> ().Speed = Speed;
     } */

    // Update is called once per frame
    void Update () {

    }
    float Speed {
        get {
            return (1 + (1 / (float) tier)) * baseSpeed;
        }
    }
    public override void OnEntityDeath (Entity entity) {
        Debug.Log ("Splitting");
        base.OnEntityDeath (entity);
        if (tier > 1) {
            float angleIncrement = 360 / spawnAmount;
            for (int i = 0; i < spawnAmount; i++) {
                GameObject g = (GameObject) Instantiate (gameObject, transform.position, Quaternion.Euler (transform.forward));
                g.GetComponent<SplitterZombie> ().tier--;
                /*  Vector3 pushVector = Quaternion.Euler (0, angleIncrement * i, 0) * transform.right;
                 g.GetComponent<Rigidbody> ().AddForce (pushVector, ForceMode.Impulse); */

            }
        }
    }
}