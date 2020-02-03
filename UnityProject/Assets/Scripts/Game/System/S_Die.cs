using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
public class S_Die : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery m_RemoveAllMecha;
    
    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        m_RemoveAllMecha = GetEntityQuery(ComponentType.ReadOnly<T_IsDead>() ,ComponentType.Exclude<T_Mecha>());
    }
    public struct DieJob : IJobForEachWithEntity<T_IsDead>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly]ref T_IsDead tIsDead)
        {
            
            eEntityCommandBuffer.DestroyEntity(index, entity);
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new DieJob
        {
            eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
        }.Schedule(m_RemoveAllMecha, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
    
}
