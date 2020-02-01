
using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

public class S_CheckIfIsDead : JobComponentSystem
{

    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery m_GetAliveEntitiesNotDeadQuery;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        m_GetAliveEntitiesNotDeadQuery = GetEntityQuery(ComponentType.ReadOnly<C_Life>(), ComponentType.Exclude<T_IsDead>());
    }

    public struct CheckLifeJob : IJobForEachWithEntity<C_Life>
    {
        [ReadOnly]public EntityCommandBuffer EntityCommandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly]ref C_Life c_Life)
        {
            if (c_Life.ActualLife <= 0)
            {
                EntityCommandBuffer.AddComponent(entity, new T_IsDead());
            }
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        
        JobHandle jobHandle = new CheckLifeJob
        {
            EntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer()
        }.Schedule(m_GetAliveEntitiesNotDeadQuery, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}
