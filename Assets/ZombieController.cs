using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    EntityManager entityManager;
    public GameObject ZombiePrefab;
    
    private void Start()
    {
        entityManager = World.Active.EntityManager;
        AddZombie(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AddZombie(10);
    }

    private void AddZombie(int v)
    {
        NativeArray<Entity> entities = new NativeArray<Entity>(v, Allocator.Temp);
        entityManager.Instantiate(ZombiePrefab, entities);
        for (int i = 0; i < v; i++)
        {
            entityManager.AddComponentData(entities[i], new Translation { Value = new float3(0, 1, 0) });
            entityManager.AddComponentData(entities[i], new MoveSpeed { Value = 1f });

        }
        entities.Dispose();
    }


}
