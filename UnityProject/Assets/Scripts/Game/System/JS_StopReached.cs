using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;


[UpdateAfter(typeof(JS_DidReach))]
public class JS_StopReached : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var pRemoveComponents = RemoveReachedComponents(inputDeps);
        pRemoveComponents.Complete();
        return pRemoveComponents;
    }
    private JobHandle RemoveReachedComponents(JobHandle inputDeps)
    {
        if (m_EndSimulationEntityCommandBufferSystem == null)
            m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();

        var pBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
        return
            Entities.ForEach((int entityInQueryIndex, Entity pEntity, in C_Move pMove, in C_ReachTarget cReachTarget, in C_TargetReached pTargetReached) =>
            {
                pBuffer.RemoveComponent(entityInQueryIndex, pEntity, typeof(C_Move));
            })
            .WithoutBurst()
            .Schedule(inputDeps);
    }
}
