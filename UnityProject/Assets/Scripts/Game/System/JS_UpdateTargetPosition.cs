using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;

class JS_UpdateTargetPosition : JobComponentSystem
{
    EntityQuery m_MechasQuery;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_MechasQuery = GetEntityQuery(new ComponentType[] { typeof(C_MechaPart), typeof(Translation) });
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float fDelta = Time.DeltaTime;
        return UpdateTargetPosition(inputDeps);
    }
    private JobHandle UpdateTargetPosition(JobHandle inputDeps)
    {
        var naMechaTranslation = m_MechasQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
        var naMechaPart = m_MechasQuery.ToComponentDataArray<C_MechaPart>(Allocator.TempJob);
        
        NativeArray<Entity> tTargets = m_MechasQuery.ToEntityArray(Allocator.TempJob);// ToComponentDataArray<Entity>(Allocator.TempJob);
        var pJob =
            Entities.ForEach((ref C_ReachTarget pTarget, in C_MechaPart pMechaPart) =>
            {
                for (int i = 0; i < naMechaPart.Length ; i++)
                {
                    if( pMechaPart.MechaPart == naMechaPart[i].MechaPart)
                    {
                        pTarget.TargetPosition = naMechaTranslation[i].Value;
                        break;
                    }
                }
            })
            .Schedule(inputDeps);
        var pJobHandle1 = tTargets.Dispose(pJob);
        var pJobHandle2 = naMechaTranslation.Dispose(pJob);
        var pJobHandle3 = naMechaPart.Dispose(pJob);
        return JobHandle.CombineDependencies(pJobHandle1, pJobHandle2, pJobHandle3);
        //return pJob;
    }
}