using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public struct PrefabSpawnerProperties : IComponentData
{
}

public struct PrefabSpawnerBuffer : IBufferElementData
{
    public int index;
    public Entity entity;
}

public class PrefabSpawnerAuthoring : MonoBehaviour
{
    public List<GameObject> debugEntities;

    public class PrefabSpawnerAuthoringBaker : Baker<PrefabSpawnerAuthoring>
    {
        public override void Bake(PrefabSpawnerAuthoring authoring)
        {
            // Add properties   
            AddComponent(new PrefabSpawnerProperties());

            AddBuffer<PrefabSpawnerBuffer>();

            for (int ii = 0; ii < authoring.debugEntities.Count; ii++)
            {
                var bufferItem = new PrefabSpawnerBuffer()
                {
                    index = ii,
                    entity = GetEntity(authoring.debugEntities[ii])
                };

                this.AppendToBuffer(bufferItem);
            }
        }
    }
}
