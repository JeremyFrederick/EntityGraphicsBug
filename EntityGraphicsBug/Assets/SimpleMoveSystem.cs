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
        //positionLookup.Update(ref state);
        //starshipMovePropertiesLookup.Update(ref state);

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