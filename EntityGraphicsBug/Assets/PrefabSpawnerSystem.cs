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

public struct PrefabSpawnRequest : IComponentData
{
    public int index;
}

[UpdateInGroup(typeof(InitializationSystemGroup))]
//[BurstCompile]
public partial struct PrefabSpawnSystem : ISystem
{
    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = SystemAPI
            .GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);

        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (spawnRequest, entity) in
            SystemAPI.Query<RefRO<PrefabSpawnRequest>>()
                .WithEntityAccess())
        {
            Entity entityPrefab = Entity.Null;
            var starshipDataLookupProperties = SystemAPI.GetSingletonEntity<PrefabSpawnerProperties>();
            var defBuffer = SystemAPI.GetBuffer<PrefabSpawnerBuffer>(starshipDataLookupProperties);
            foreach (var definition in defBuffer)
            {
                if (definition.index == spawnRequest.ValueRO.index)
                {
                    entityPrefab = definition.entity;
                    break;
                }
            }

            // Destroy since we processed
            ecb.DestroyEntity(entity);

            if (entityPrefab == Entity.Null) break;

            Entity spawn = state.EntityManager.Instantiate(entityPrefab);
            //state.EntityManager.SetComponentData<LocalTransform>(spawn, new LocalTransform()
            //{
            //    Position = fleetSrc.Transform.position,
            //    Rotation = fleetSrc.Transform.rotation,
            //    Scale = 1f
            //});

            //state.EntityManager.SetComponentData<WorldTransform>(spawn, new WorldTransform()
            //{
            //    Position = fleetSrc.Transform.position,
            //    Rotation = fleetSrc.Transform.rotation,
            //    Scale = 1f
            //});
        }
    }

    public static void SpawnPrefab(World world, int index)
    {
        Entity createdSpawnEntity = world.EntityManager.CreateEntity();
        world.EntityManager.AddComponentData(createdSpawnEntity, new PrefabSpawnRequest
        {
            index = index
        });
    }

}