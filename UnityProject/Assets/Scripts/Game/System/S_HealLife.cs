using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

public class S_HealLife : JobComponentSystem
{
    
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    
    public struct HealLifeJob : IJobForEachWithEntity<C_Life, C_LifeToHeal>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        public void Execute(Entity entity, int index, ref C_Life cLife, [ReadOnly]ref C_LifeToHeal cLifeToHeal)
        {
            cLife.ActualLife = math.max(cLife.ActualLife + cLifeToHeal.LifeToHeal, cLife.MaxLife);
            eEntityCommandBuffer.RemoveComponent<C_LifeToHeal>(index, entity);
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new HealLifeJob
            {eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()}.Schedule(this, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}
