using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class S_Die : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    
    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    public struct DieJob : IJobForEachWithEntity<T_IsDead>
    {
        public EntityCommandBuffer eEntityCommandBuffer;
        public void Execute(Entity entity, int index, ref T_IsDead tIsDead)
        {
            eEntityCommandBuffer.DestroyEntity(entity);
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new DieJob{eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer()}.Schedule(this, inputDeps);
        return jobHandle;
    }
    
}
