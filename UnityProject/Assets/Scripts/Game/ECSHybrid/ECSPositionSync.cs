using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class ECSPositionSync : JobComponentSystem
{
    public ECSComponentMono<Transform> m_Transforms;
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((in C_SyncPositionMono pSync) =>
        {
            m_Transforms.GetObject(pSync.Id).position = pSync.Position;
        })
            .WithoutBurst()
            .Run();
        return inputDeps;
    }
}
