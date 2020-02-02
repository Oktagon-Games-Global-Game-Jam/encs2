﻿using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
[UpdateBefore(typeof(S_Die))]
public class S_EndGame : JobComponentSystem
{
    public EndSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    private EntityQuery GetEntityOnChangeVersion;
    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
         GetEntityOnChangeVersion = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadOnly<C_EndGame>(), 
        });
        GetEntityOnChangeVersion.AddChangedVersionFilter(ComponentType.ReadOnly<C_EndGame>());
    }

    public struct EndGameJob: IJobForEachWithEntity<C_EndGame>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        

        public void Execute(Entity entity, int index, [ReadOnly]ref C_EndGame tGameWon)
        {
            if (tGameWon.IsMechaWinner)
            {
                Debug.Log("GameWon");
            }else
                Debug.Log("GameLost");

        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new EndGameJob
        {
            eEntityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent(),
        }.Schedule(GetEntityOnChangeVersion, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}
