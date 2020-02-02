using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Rendering;

/*
class JS_CoinCollector : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer tCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer();
        return 
            Entities.ForEach(
                (Entity entity, ref C_SpawnData spawnData, ref Prefab prefab, in T_Ally pAlly) =>
                {
                    tCommandBuffer.DestroyEntity(entity);
                })
            .Schedule(inputDeps);
    }
}*/