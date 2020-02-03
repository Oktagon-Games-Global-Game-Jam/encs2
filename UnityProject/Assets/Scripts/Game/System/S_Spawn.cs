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
    private BeginSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery m_Query;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<BeginSimulationEntityCommandBufferSystem>();
        m_Query = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadWrite<C_SpawnRequest>(), 
        });
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        NativeArray<Entity> tEntities = m_Query.ToEntityArray(Allocator.TempJob);
        J_SpawnJob tSpawnJob = new J_SpawnJob
        {
            CommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent(),
            SpawnRequestChunk = GetArchetypeChunkComponentType<C_SpawnRequest>(),
            tEntities = tEntities
        };
        
        JobHandle tHandle = tSpawnJob.Schedule(m_Query, inputDeps);
        tHandle.Complete();

        tEntities.Dispose();
        
        return tHandle;
    }

    public struct J_SpawnJob : IJobChunk
    {
        public ArchetypeChunkComponentType<C_SpawnRequest> SpawnRequestChunk;
        public EntityCommandBuffer.Concurrent CommandBuffer;

        public NativeArray<Entity> tEntities;
        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            NativeArray<C_SpawnRequest> tSpawnRequestArray = chunk.GetNativeArray(SpawnRequestChunk);
            for (int i = 0; i < chunk.Count; i++)
            {
           
                Entity tSpawnedEntity = CommandBuffer.Instantiate(firstEntityIndex, tSpawnRequestArray[i].Reference);
                                
                CommandBuffer.SetComponent(firstEntityIndex, tSpawnedEntity, new Translation
                {
                    Value = tSpawnRequestArray[i].Position
                });
                
               
                CommandBuffer.DestroyEntity(firstEntityIndex, tEntities[firstEntityIndex + i]);
                // CommandBuffer.SetComponent(firstEntityIndex, tSpawnedEntity, new RotationEulerXYZ
                // {
                //     Value = new float3(0, 90 * tSpawnRequestArray[i].Direction, 0)
                // });
            }
        }
    }
}
