using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[UpdateBefore(typeof(S_Spawn))]
public class S_SpawnEnemy : JobComponentSystem
{
    private GameData m_GameData;
  
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        m_GameData = Object.FindObjectOfType<GameData>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float tTime = Time.DeltaTime;
        float EnemyPositionX = m_GameData.m_LevelData.m_EnemySpawnPointX;
        EntityCommandBuffer tCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer();
        int tRandomCount = 0;
        Entities.ForEach(
            (Entity entity, ref C_SpawnData spawnData, ref Prefab prefab, in T_Enemy eEnemy) =>
            {
                spawnData.TimeCache += tTime;
                if (spawnData.TimeCache > spawnData.Cooldown)
                {
                    for (int i = 0; i < spawnData.SpawnAmount; i++)
                    {
                        Entity tSpawnEntity = tCommandBuffer.CreateEntity();
                        tCommandBuffer.AddComponent(tSpawnEntity, typeof(C_SpawnRequest));
                        
                        float tX = Random.Range(spawnData.SpawnArea.x, spawnData.SpawnArea.y);
                        float tZ = Random.Range(spawnData.SpawnArea.z, spawnData.SpawnArea.w);
                        
                        tCommandBuffer.SetComponent( tSpawnEntity, new C_SpawnRequest
                        {
                            Position = new float3(EnemyPositionX + tX, (int) spawnData.MechaLane, tZ) + spawnData.Offset,
                            Direction = 1,
                            Reference = entity
                        });
                        
                    }
                    spawnData.TimeCache = 0;
                }
            })
            .WithoutBurst()
            .Run();

        return inputDeps;
    }
}
