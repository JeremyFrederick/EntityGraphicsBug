using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Profiling;
using UnityEngine.SocialPlatforms;
using Unity.Entities.UniversalDelegates;

public partial struct SimpleMoveSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // COMMENT OUT THIS ENTIRE FUNCTION AND THE BUG WILL NOT HAPPEN
        // Actually, it is the ref LocalTransform localTransform in the Execute that causes this issue. If any other component
        // is used instead of LocalTransform, memory will be allocated on each spawn but it will not continue to grow until it crashes
        float deltaTime = SystemAPI.Time.DeltaTime;
        if (deltaTime > 0f)
        {
            // Move
            state.Dependency = new SimpleMoveJob
            {
            }.ScheduleParallel(state.Dependency);
        }
    }


    [BurstCompile]
    partial struct SimpleMoveJob : IJobEntity
    {
        void Execute(ref LocalTransform localTransform)
        {
        }
    }
}