using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

public class S_RepairLife : JobComponentSystem
{
    
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    
    public struct RepairJob : IJobForEachWithEntity<C_Life, C_RepairValue>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        public void Execute(Entity entity, int index, ref C_Life cLife, [ReadOnly]ref C_RepairValue cRepair)
        {
            cLife.ActualLife = math.min(cLife.ActualLife + cRepair.AmoutToRepair, cLife.MaxLife);
            eEntityCommandBuffer.RemoveComponent<C_RepairValue>(index, entity);
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (m_EndSimulationEntityCommandBufferSystem == null)
            m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();

        JobHandle jobHandle = new RepairJob
            {eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()}.Schedule(this, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}
