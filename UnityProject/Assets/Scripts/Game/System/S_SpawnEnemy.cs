﻿using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
[UpdateBefore(typeof(S_Spawn))]
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
        EntityCommandBuffer.Concurrent tCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
        JobHandle tJobHandle = Entities.ForEach(
            (int entityInQueryIndex, Entity entity, ref C_Move move, ref Prefab prefab) =>
            {
                for (int i = 0; i < 10; i++) 
                {
                    Entity tSpawnEntity = tCommandBuffer.CreateEntity(entityInQueryIndex);
                    tCommandBuffer.AddComponent(entityInQueryIndex, tSpawnEntity, typeof(C_SpawnRequest));
                    tCommandBuffer.SetComponent(entityInQueryIndex, tSpawnEntity, new C_SpawnRequest
                    {
                        Position = float3.zero,
                        Direction = 1,
                        Reference = entity
                    });
                }
            })
            .WithoutBurst()
            .Schedule(inputDeps);
        tJobHandle.Complete();
        return tJobHandle;
    }
}
