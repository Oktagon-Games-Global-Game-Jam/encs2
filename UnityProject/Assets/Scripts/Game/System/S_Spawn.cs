using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class S_Spawn : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery m_Query;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        m_Query = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadWrite<C_SpawnRequest>(), 
        });
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        
        J_SpawnJob tSpawnJob = new J_SpawnJob
        {
            CommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer(),
            SpawnRequestChunk = GetArchetypeChunkComponentType<C_SpawnRequest>()
        };

        return tSpawnJob.Schedule(m_Query, inputDeps);
    }

    public struct J_SpawnJob : IJobChunk
    {
        public ArchetypeChunkComponentType<C_SpawnRequest> SpawnRequestChunk;
        public EntityCommandBuffer CommandBuffer;
        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            NativeArray<C_SpawnRequest> tSpawnRequestArray = chunk.GetNativeArray(SpawnRequestChunk);
            for (int i = 0; i < chunk.Count; i++)
            {
                Entity tSpawnedEntity = CommandBuffer.Instantiate(tSpawnRequestArray[i].Reference);
                
                CommandBuffer.SetComponent(tSpawnedEntity, new Translation
                {
                    Value = tSpawnRequestArray[i].Position
                });
                CommandBuffer.SetComponent(tSpawnedEntity, new RotationEulerXYZ
                {
                    Value = new float3(0, 90 * tSpawnRequestArray[i].Direction, 0)
                });
            }
        }
    }
}
