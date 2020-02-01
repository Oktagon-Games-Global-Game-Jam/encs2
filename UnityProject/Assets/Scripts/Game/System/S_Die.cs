using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
public class S_Die : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    
    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    public struct DieJob : IJobForEachWithEntity<T_IsDead>
    {
        [ReadOnly]public EntityCommandBuffer eEntityCommandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly]ref T_IsDead tIsDead)
        {
            
            eEntityCommandBuffer.DestroyEntity(entity);
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new DieJob{eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer()}.Schedule(this, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
    
}
