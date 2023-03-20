using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public class DebugSceneController : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            int index = 0;
            Debug.LogFormat("Spawning Index: {0}", index);
            PrefabSpawnSystem.SpawnPrefab(World.DefaultGameObjectInjectionWorld, index);
        }
    }
}
