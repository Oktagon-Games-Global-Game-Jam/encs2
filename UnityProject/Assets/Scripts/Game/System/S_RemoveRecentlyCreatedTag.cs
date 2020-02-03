using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class S_RemoveRecentlyCreatedTag : JobComponentSystem
{
    public BeginInitializationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer.Concurrent tCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
        var job = Entities.ForEach((int entityInQueryIndex, Entity entity, ref T_RecentlyCreated recentlyCreated) =>
        {
            tCommandBuffer.RemoveComponent<T_RecentlyCreated>(entityInQueryIndex, entity);   
        }).Schedule(inputDeps);
        job.Complete();
        return job;
    }
}