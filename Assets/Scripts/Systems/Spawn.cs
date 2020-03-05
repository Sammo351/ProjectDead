using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Spawn {
    /*Spawn enemies within a radius to designted position.
    Enemies will not engage unless stiumlated.*/
    public static void Surround (Vector3 position, int count = 1, float radius = 35f, bool attack = false) {

        List<EnemySpawner> list = GetSpawners (position, radius);
        for (int i = 0; i < count; i++) {
            int rand = Random.Range (0, list.Count);
            if (attack) {
                list[rand].Spawn (1, position);
            } else {
                list[rand].Spawn (1);
            }
        }
    }

    public static List<EnemySpawner> GetSpawners (Vector3 position, float radius) {
        List<EnemySpawner> list = GameObject.FindObjectsOfType<EnemySpawner> ().ToList ();
        List<EnemySpawner> output = new List<EnemySpawner> ();
        output = list.Where ((x) => Vector3.Distance (x.transform.position, position) <= radius).ToList ();
        return output;
    }
}