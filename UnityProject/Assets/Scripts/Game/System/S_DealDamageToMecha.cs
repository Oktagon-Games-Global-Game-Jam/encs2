using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class S_DealDamageToMecha : JobComponentSystem
{
    
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private EntityQuery m_GetMechaParts;

    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        m_GetMechaParts = GetEntityQuery(typeof(T_Mecha), ComponentType.ReadOnly<C_MechaPart>());
    }

    public struct DealDamageToMechaJob : IJobForEachWithEntity<C_DamageMecha, C_MechaPart>
    {
        public EntityCommandBuffer.Concurrent eConcurrent;
        public NativeArray<Entity> mechaEntities;
        public NativeArray<C_MechaPart> mechaParts;

        public void Execute(Entity entity, int index, [ReadOnly]ref C_DamageMecha cDamageMecha, [ReadOnly]ref C_MechaPart cMechaPart)
        {
            for (int i = 0; i < mechaParts.Length; i++)
            {
                if (mechaParts[i].MechaPart == cMechaPart.MechaPart)
                {
                    eConcurrent.AddComponent(index, mechaEntities[i], new C_DamageToTake{DamageToTake = cDamageMecha.DamageToDeal});
                    eConcurrent.DestroyEntity(index,entity);
                    break;
                }
            }

        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        NativeArray<Entity> mechas = m_GetMechaParts.ToEntityArray(Allocator.TempJob);
        NativeArray<C_MechaPart> mechaParts = m_GetMechaParts.ToComponentDataArray<C_MechaPart>(Allocator.TempJob);
        JobHandle jobHandle = new DealDamageToMechaJob
        {
            eConcurrent = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent(),
            mechaEntities = mechas,
            mechaParts = mechaParts
        }.Schedule(this, inputDeps);
        mechas.Dispose();
        mechaParts.Dispose();
        return jobHandle; 
    }
}
