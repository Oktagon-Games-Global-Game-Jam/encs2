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
        if (m_Transforms == null)
            return inputDeps;
        Entities.ForEach((in C_SyncPositionMono pSync) =>
        {
            var p = m_Transforms.GetObject(pSync.Id);
            if(p)
                m_Transforms.GetObject(pSync.Id).position = pSync.Position;
        })
            .WithoutBurst()
            .Run();
        return inputDeps;
    }
}
