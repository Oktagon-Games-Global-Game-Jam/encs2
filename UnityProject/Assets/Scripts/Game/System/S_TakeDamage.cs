using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

public class S_TakeDamage : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery GetEntitiesNotDead;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        GetEntitiesNotDead = GetEntityQuery(typeof(C_Life), ComponentType.ReadOnly<C_DamageToTake>(),
            ComponentType.Exclude<T_IsDead>());
    }

    public struct TakeDamageJob: IJobForEachWithEntity<C_Life, C_DamageToTake>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        public void Execute(Entity entity, int index, ref C_Life cLife, [ReadOnly]ref C_DamageToTake cDamageToTake)
        {
            cLife.ActualLife = math.max(0, cLife.ActualLife - cDamageToTake.DamageToTake);
            eEntityCommandBuffer.RemoveComponent<C_DamageToTake>(index, entity);
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (m_EndSimulationEntityCommandBufferSystem == null)
            m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();

        JobHandle jobHandle = new TakeDamageJob
        {
            eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
        }.Schedule(GetEntitiesNotDead, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}
