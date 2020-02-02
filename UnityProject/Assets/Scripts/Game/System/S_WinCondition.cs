using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class S_WinCondition : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery m_GetNotWonMechas;
    private GameData m_GameData;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        m_GetNotWonMechas = GetEntityQuery(ComponentType.ReadOnly<T_Mecha>(), ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<C_MechaPart>(),
            ComponentType.Exclude<C_EndGame>());
        
    }

    protected override void OnStartRunning()
    {
        m_GameData = Object.FindObjectOfType<GameData>();
    }

    public struct WonGameCondition : IJobForEachWithEntity<T_Mecha, Translation, C_MechaPart>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        public float endXPosition;
        public void Execute(Entity entity, int index, [ReadOnly]ref T_Mecha tMecha, [ReadOnly]ref Translation tTranslation, [ReadOnly]ref C_MechaPart cMechaPart)
        {
            if (Math.DistanceXZ(endXPosition, tTranslation.Value) <= 1.5f)
            {
                eEntityCommandBuffer.AddComponent(index, entity, new C_EndGame{IsMechaWinner = true});
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new WonGameCondition
        {
            eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent(),
            endXPosition = m_GameData.m_LevelData.m_EnemySpawnPointX,
        }.Schedule(m_GetNotWonMechas);
        return jobHandle;
    }
}
