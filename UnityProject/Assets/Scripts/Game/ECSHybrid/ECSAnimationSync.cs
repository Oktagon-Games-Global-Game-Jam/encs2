﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class ECSAnimationSync : JobComponentSystem
{
    private ECSComponentMono<Animator> m_Animations;
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        m_Animations = new ECSComponentMono<Animator>(Object.FindObjectsOfType<Animator>());    
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((in C_SyncAnimationMono pSync) =>
            {
                m_Animations.GetObject(pSync.Id).Play(pSync.State.ToString());
            })
            .WithoutBurst()
            .Run();
        return inputDeps;
    }
}
