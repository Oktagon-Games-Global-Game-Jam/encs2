using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class S_CameraFollowPosition : JobComponentSystem
{
    private EntityQuery m_Query;

    protected override void OnCreate()
    {
        base.OnCreate();
        m_Query = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadOnly<T_Camera>(),
            ComponentType.ReadOnly<C_ReachTarget>(),
            ComponentType.ReadWrite<C_SyncPositionMono>(),
            ComponentType.ReadWrite<Translation>(),
        });
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        JobHandle tJobHandle = Entities.ForEach(
                (ref Translation translation, ref C_SyncPositionMono syncPositionMono, in C_ReachTarget reachTarget,
                    in T_Camera camera) =>
                {
                    var tTranslation = math.lerp(translation.Value, reachTarget.TargetPosition, 0.5f);
                    tTranslation.z = translation.Value.z;
                    translation.Value = tTranslation;
                    syncPositionMono.Position = tTranslation;
                })
            .WithoutBurst()
            .Schedule(m_Query.GetDependency());

        return JobHandle.CombineDependencies(inputDeps, tJobHandle);
    }



}
