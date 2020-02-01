using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class ECSPositionSync : JobComponentSystem
{
    private ECSComponentMono<Transform> m_Transforms;
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        m_Transforms = new ECSComponentMono<Transform>(Object.FindObjectsOfType<Transform>());    
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
