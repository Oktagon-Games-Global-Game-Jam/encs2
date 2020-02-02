using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;


public class JS_Move : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float fDelta = Time.DeltaTime;
        return MoveSchedule(inputDeps, fDelta);
    }

    #region 
    private JobHandle MoveSchedule(JobHandle inputDeps, float fDelta)
    {
        // move
        return
            Entities.ForEach((ref Translation pTranslation, in Rotation pRotation, in C_Move cMove) =>
            {
                pTranslation.Value = pTranslation.Value + cMove.Speed * math.forward(pRotation.Value);
            })
            .Schedule(inputDeps);
    }
    #endregion
}
