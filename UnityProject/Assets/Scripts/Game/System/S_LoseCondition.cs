using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

public class S_LoseCondition : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery m_ExcludeAlreadyWonEntities;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        m_ExcludeAlreadyWonEntities = GetEntityQuery(ComponentType.ReadOnly<T_Mecha>(),
            ComponentType.ReadOnly<T_IsDead>(), ComponentType.Exclude<C_EndGame>());
    }

    public struct LoseJob: IJobForEachWithEntity<T_Mecha, T_IsDead>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly]ref T_Mecha mecha, [ReadOnly]ref T_IsDead isDead)
        {
            eEntityCommandBuffer.AddComponent(index ,entity, new C_EndGame{IsMechaWinner = false});
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new LoseJob{eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()}.Schedule(m_ExcludeAlreadyWonEntities);
        jobHandle.Complete();
        return jobHandle;
    }
}