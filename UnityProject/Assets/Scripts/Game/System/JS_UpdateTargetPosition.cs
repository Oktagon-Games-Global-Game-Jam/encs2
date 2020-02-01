using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
/*
class JS_UpdateTargetPosition : JobComponentSystem
{
    EntityQuery m_MechasQuery;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_MechasQuery = GetEntityQuery(new ComponentType[] { typeof(T_Target), typeof(Translation) });
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float fDelta = Time.DeltaTime;
        return UpdateTargetPosition(inputDeps);
    }
    private JobHandle UpdateTargetPosition(JobHandle inputDeps)
    {
        NativeArray<Translation> tTargets = m_MechasQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
        var pJob =
            Entities.ForEach((ref Translation pTranslation, in Rotation pRotation, in C_Move cMove) =>
            {
                for (int i = 0; i < tTargets.Length; i++)
                {
                    pTranslation.Value = pTranslation.Value + cMove.Speed * math.forward(pRotation.Value);
                }
            })
            .Schedule(inputDeps);
        tTargets.Dispose();
        return pJob;
    }
}*/