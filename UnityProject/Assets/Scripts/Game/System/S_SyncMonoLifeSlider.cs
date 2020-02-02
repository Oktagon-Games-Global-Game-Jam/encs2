using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class S_SyncMonoLifeSlider : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return Entities.ForEach((ref C_SyncSlider sync, ref C_Life life) =>
        {
            sync.Value = (float)life.ActualLife/ (float)life.MaxLife;
        }).Schedule(inputDeps);
    }
}
