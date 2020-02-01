using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

public class S_TakeDamage : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    public struct TakeDamageJob: IJobForEachWithEntity<C_Life, C_DamageToTake>
    {
        [ReadOnly]public EntityCommandBuffer eEntityCommandBuffer;
        public void Execute(Entity entity, int index, ref C_Life cLife, [ReadOnly]ref C_DamageToTake cDamageToTake)
        {
            cLife.ActualLife = math.min(0, cLife.ActualLife - cDamageToTake.DamageToTake);
            if (cLife.ActualLife == 0)
            {
                eEntityCommandBuffer.AddComponent(entity, new T_IsDead());
            }
            eEntityCommandBuffer.RemoveComponent<C_DamageToTake>(entity);
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new TakeDamageJob{ eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer()}.Schedule(this, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}
