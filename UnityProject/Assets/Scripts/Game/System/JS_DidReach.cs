using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;

[UpdateAfter(typeof(JS_Move))]
class JS_DidReach : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var pReach = DidReachSchedule(inputDeps);
        pReach.Complete();
        return pReach;
    }

    private JobHandle DidReachSchedule(JobHandle inputDeps)
    {
        var pBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
        return
            Entities.ForEach((int entityInQueryIndex, Entity pEntity, ref C_ReachTarget cReachTarget, in Translation pTrans) =>
            {
                if (Math.DistanceXZ(cReachTarget.TargetPosition, pTrans.Value) <= cReachTarget.ReachDistance)
                {
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new C_TargetReached());
                }
            })
            .Schedule(inputDeps);
    }
}