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
    private GameData m_GameData;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_InputsQuery = GetEntityQuery(new ComponentType[] { typeof(C_InputData) });
        m_GameData = Object.FindObjectOfType<GameData>();
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var tInputData = m_InputsQuery.ToComponentDataArray<C_InputData>(Allocator.TempJob);
        float tLeftBorder = m_GameData.m_LevelData.m_PlayerSpawnPointX;
        float tRightBorder = m_GameData.m_LevelData.m_EnemySpawnPointX;
        var pJob =
            Entities.ForEach((ref C_ReachTarget pTarget, ref T_CameraFollowData pFollowData) =>
                {
                    if(pFollowData.FollowType == E_FollowType.Mecha)
                        return;
                    
                    for (int i = 0; i < tInputData.Length ; i++)
                    {
                        pTarget.TargetPosition += new float3(tInputData[i].DeltaMove.x, 0, 0) * 0.05f;
                        pTarget.TargetPosition = math.clamp(pTarget.TargetPosition, tLeftBorder, tRightBorder);
                    }
                })
                .Schedule(inputDeps);
        pJob.Complete();
        tInputData.Dispose();
        return pJob;
    }

}