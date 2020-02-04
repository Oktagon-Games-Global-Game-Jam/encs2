using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
[UpdateBefore(typeof(S_Die))]
public class S_SpawnParticleOnUnitDie : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery m_KillQuery;

    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        m_KillQuery = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadOnly<C_Unit>(),
            ComponentType.ReadOnly<T_IsDead>(),
        });

        m_KillQuery.AddChangedVersionFilter(ComponentType.ReadOnly<T_IsDead>());
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (m_EndSimulationEntityCommandBufferSystem == null)
            m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();

        var pBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
        var pTask =
            Entities
                .WithoutBurst()
                .ForEach((int entityInQueryIndex, in T_IsDead tIsDead, in Translation pTranslation, in C_Unit unit) =>
                {
                    var pEntity = pBuffer.CreateEntity(entityInQueryIndex);
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new C_Particle{id = unit.ParticleSystemReference, Count = 1});
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new Translation{Value = pTranslation.Value});
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new LocalToWorld());
                    
                })
                .Schedule(m_KillQuery.GetDependency());
        pTask.Complete();
        return pTask;
    }
}
