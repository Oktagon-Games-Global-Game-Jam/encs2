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
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float fDelta = Time.DeltaTime;


        var pMove = MoveSchedule(inputDeps, fDelta);


        return pMove;
    }


    private JobHandle MoveSchedule(JobHandle inputDeps, float fDelta)
    {
        // move
        return 
        Entities.ForEach((ref C_Move cMove, ref Translation pTranslation, ref Rotation pRotation) =>
        {
            pTranslation.Value = pTranslation.Value + cMove.Speed * math.forward(pRotation.Value);
        })
        .Schedule(inputDeps);
    }


}
