using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class S_SpawnEnemy : JobComponentSystem
{
    private float m_Cooldown = 5;
    private float m_Time = 0;

    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        m_Time += Time.DeltaTime;
        
        if (m_Time < m_Cooldown)
            return inputDeps;
        
        m_Time = 0;
        EntityCommandBuffer tCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer();
        return Entities.ForEach((Entity entity, ref Prefab prefab) =>
        {
            for (int i = 0; i < 10; i++)
            {
                Entity tSpawnEntity =tCommandBuffer.CreateEntity();
                tCommandBuffer.AddComponent(tSpawnEntity, typeof(C_SpawnRequest));
                tCommandBuffer.SetComponent(tSpawnEntity, new C_SpawnRequest
                {
                    Position = float3.zero,
                    Direction = unchecked((byte)-1),
                    Reference = entity
                });
            }
        }).Schedule(inputDeps);
    }
}
