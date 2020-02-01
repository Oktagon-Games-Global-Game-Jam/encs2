using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;

class S_UpdateTargetMousePosition : JobComponentSystem
{
    EntityQuery m_InputsQuery;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_InputsQuery = GetEntityQuery(new ComponentType[] { typeof(C_InputData) });
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var tInputData = m_InputsQuery.ToComponentDataArray<C_InputData>(Allocator.TempJob);
        
        var pJob =
            Entities.ForEach((ref C_ReachTarget pTarget, ref T_CameraFollowData pFollowData) =>
                {
                    if(pFollowData.FollowType == E_FollowType.Mecha)
                        return;
                    
                    for (int i = 0; i < tInputData.Length ; i++)
                    {
                        pTarget.TargetPosition += new float3(tInputData[i].DeltaMove.x, 0, 0) * 0.05f;
                    }
                })
                .Schedule(inputDeps);
        pJob.Complete();
        tInputData.Dispose();
        return pJob;
    }

}