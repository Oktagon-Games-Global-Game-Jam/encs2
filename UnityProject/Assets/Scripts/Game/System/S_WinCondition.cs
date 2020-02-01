using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

public class S_WinCondition : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery m_GetNotWonMechas;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        m_GetNotWonMechas = GetEntityQuery(ComponentType.ReadOnly<T_Mecha>(), ComponentType.ReadOnly<C_TargetReached>(),
            ComponentType.Exclude<T_GameWon>());
    }
    
    public struct WonGameCondition : IJobForEachWithEntity<T_Mecha, C_TargetReached>
    {
        [ReadOnly]public EntityCommandBuffer eEntityCommandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly]ref T_Mecha tMecha, [ReadOnly]ref C_TargetReached cTargetReached)
        {
            eEntityCommandBuffer.AddComponent<T_GameWon>(entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new WonGameCondition{eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer()}.Schedule(m_GetNotWonMechas);
        return jobHandle;
    }
}
