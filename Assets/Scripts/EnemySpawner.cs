using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{

    public Enemies enemies; //= (Enemies) ~0;
    public Vector3 destination;
    void Reset()
    {
        enemies = Ext.GetAllowedEnemies();
        destination = transform.position.AdjustX(-3).AdjustZ(-3);
    }
    void OnValidate()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Spawn(int num = 1, Vector3? dest = null)
    {
        Enemies? enemy = RandomEnemy();
        if (enemy != null)
        {
            SpawnSpecific(RandomEnemy().Value, num, dest);
        }
    }
    public void SpawnSpecific(Enemies enemy, int num = 1, Vector3? dest = null, float p = 0)
    {
        GameObject g = World.GetEnemyObject(enemy);
        GameObject entity = (GameObject)Instantiate(g, transform.position, Quaternion.identity);
        entity.GetComponent<AI>().SetTarget(dest != null ? dest.Value : destination, p);
    }
    public void SpawnWithPriority(int num = 1, float priority = 0, Vector3? dest = null)
    {
        Enemies? enemy = RandomEnemy();
        if (enemy != null)
        {

            SpawnSpecific(RandomEnemy().Value, num, dest, priority);
        }
    }

    [ContextMenu("Place")]
    void Place()
    {
        RaycastHit hit;
        Vector3 origin = transform.position - (Vector3.up * (transform.localScale.y + 0.001f));
        if (Physics.Raycast(origin, Vector3.down, out hit, 10f))
        {
            transform.position = hit.point.AdjustY(transform.localScale.y);
        }
    }
    Enemies? RandomEnemy()
    {
        var matching = Enum.GetValues(typeof(Enemies))
            .Cast<Enemies>()
            .Where(c => (enemies & c) == c && Ext.IsEnemyAllowedToSpawn(c)) // or use HasFlag in .NET4
            .ToArray();
        if (matching.Length >= 1)
        {
            var ret = matching[UnityEngine.Random.Range(0, matching.Length)];
            return ret;
        }
        return null;
    }

}