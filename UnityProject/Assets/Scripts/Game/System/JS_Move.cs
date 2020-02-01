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
    EntityQuery eq;
    protected override void OnCreate()
    {
        base.OnCreate();
        eq = GetEntityQuery(new ComponentType[] { typeof(C_ReachTarget) });
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float fDelta = Time.DeltaTime;

        var pMove = MoveSchedule(inputDeps, fDelta);
        var pReach = DidReachSchedule(pMove);
        pReach.Complete();

        return pReach;// JobHandle.CombineDependencies(pMove, pReach);
    }


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
    private JobHandle DidReachSchedule(JobHandle inputDeps)
    {
        var pBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
        return
            Entities.ForEach((int entityInQueryIndex, Entity pEntity, ref C_ReachTarget cReachTarget, in Translation pTrans) =>
            {
                if (math.distance(cReachTarget.TargetPosition, pTrans.Value) <= cReachTarget.ReachDistance)
                {
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new C_TargetReached());
                }
            })
            .Schedule(inputDeps);
    }
    private JobHandle DidReachCheckWIP(JobHandle inputDeps, float fDelta)
    {
        //NativeArray<C_ReachTarget> tTargets = eq.ToComponentDataArray<C_ReachTarget>(Allocator.TempJob);
        //NativeArray<C_ReachTarget> tReachTargets = tTargets;
        //return
        //    Entities.ForEach((ref Translation pTranslation, in Rotation pRotation, in C_Move cMove) =>
        //    {
        //        for (int i = 0; i < tTargets.Length; i++)
        //        {
        //            pTranslation.Value = pTranslation.Value + cMove.Speed * math.forward(pRotation.Value);
        //        }
        //    })
        //    .Complete(inputDeps);

        // move
        return
            Entities.ForEach((ref Translation pTranslation, in Rotation pRotation, in C_Move cMove) =>
            {
                pTranslation.Value = pTranslation.Value + cMove.Speed * math.forward(pRotation.Value);
            })
            .Schedule(inputDeps);
    }
}
