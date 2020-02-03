using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[UpdateBefore(typeof(S_Spawn))]
class JS_SpawnReparer : JobComponentSystem  
{
    private GameData m_GameData;
    EntityQuery uiQuery;
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        uiQuery = GetEntityQuery(typeof(C_UISpawnUnitRequest));
    }



    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        m_GameData = Object.FindObjectOfType<GameData>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var naMechaTranslation = uiQuery.ToComponentDataArray<C_UISpawnUnitRequest>(Allocator.TempJob);
        if (naMechaTranslation.Length > 0)
        {
            int Resource = World.GetExistingSystem<S_CreditResourceOnRetrive>().AmountResource;
            float tTime = Time.DeltaTime;
            float PlayerPositionX = m_GameData.m_LevelData.m_PlayerSpawnPointX;
            EntityCommandBuffer tCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer();
            
            int tRandomCount = 0;
            if (Resource > 0)
            {

                Entities.ForEach(
                        (Entity entity, ref C_SpawnData spawnData, ref Prefab prefab, in T_Ally pAlly) =>
                        {
                            bool bPass = false;
                            for (int i = 0; i < naMechaTranslation.Length; i++)
                            {
                                if (naMechaTranslation[i].MechaPart == spawnData.MechaLane)
                                    bPass = true;
                            }

                            if (!bPass)
                                return;
                            for (int i = 0; i < spawnData.SpawnAmount; i++)
                            {
                                Entity tSpawnEntity = tCommandBuffer.CreateEntity();
                                tCommandBuffer.AddComponent(tSpawnEntity, typeof(C_SpawnRequest));

                                float tX = Random.Range(spawnData.SpawnArea.x, spawnData.SpawnArea.y);
                                float tZ = Random.Range(spawnData.SpawnArea.z, spawnData.SpawnArea.w);

                                tCommandBuffer.SetComponent(tSpawnEntity, new C_SpawnRequest
                                {
                                    Position = new float3(PlayerPositionX + tX, (int) spawnData.MechaLane, tZ) +
                                               spawnData.Offset,
                                    Direction = 1,
                                    Reference = entity
                                });
                            }
                        })
                    .WithoutBurst()
                    .Run();
                World.GetExistingSystem<S_CreditResourceOnRetrive>().AmountResource--;
            }
        }
        naMechaTranslation.Dispose();
        return inputDeps;
    }
}
