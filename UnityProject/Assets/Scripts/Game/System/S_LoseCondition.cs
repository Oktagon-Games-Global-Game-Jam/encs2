using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
[UpdateBefore(typeof(S_Die))]
public class S_LoseCondition : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    public struct LoseJob: IJobForEachWithEntity<T_Mecha, T_IsDead>
    {
        [ReadOnly]public EntityCommandBuffer eEntityCommandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly]ref T_Mecha mecha, [ReadOnly]ref T_IsDead isDead)
        {
            Entity newEntity = eEntityCommandBuffer.CreateEntity();
            eEntityCommandBuffer.AddComponent<T_LostGame>(newEntity);
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new LoseJob{eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer()}.Schedule(this, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}
