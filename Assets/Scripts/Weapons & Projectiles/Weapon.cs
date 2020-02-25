using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;
    public Transform SpawnPoint;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var go = Instantiate(Bullet) as GameObject;
            go.transform.position = SpawnPoint.position;
            go.transform.forward = SpawnPoint.forward;
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 50, ForceMode.Impulse);

        }
    }
}
