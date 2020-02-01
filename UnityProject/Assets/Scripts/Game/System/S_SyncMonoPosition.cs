using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class S_SyncMonoPosition : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return Entities.ForEach((ref C_SyncPositionMono sync, ref Translation translation) =>
            {
                sync.Position = translation.Value;
            }).Schedule(inputDeps);
    }
}
