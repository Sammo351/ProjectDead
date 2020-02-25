
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MovementSystem : JobComponentSystem
{
    struct MovementJob : IJobForEach<Translation, MoveSpeed>
    {
        public float deltaTime;

        public void Execute(ref Translation c0, ref MoveSpeed c1)
        {
            float3 value = c0.Value;
            value += deltaTime * c1.Value;

            c0.Value = value;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        MovementJob moveJob = new MovementJob
        {
            deltaTime = Time.deltaTime
        };

        JobHandle moveHandle = moveJob.Schedule(this);
        
        return moveHandle;

    }
}

