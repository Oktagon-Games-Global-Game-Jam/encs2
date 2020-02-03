using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
[UpdateAfter(typeof(S_EndGame))]
public class ECSAnimationSync : JobComponentSystem
{
    public ECSComponentMono<Animator> m_Animations;
    private E_State m_LastState;
    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((in C_SyncAnimationMono pSync) =>
            {
                if (m_LastState != pSync.State)
                {
                    m_LastState = pSync.State;
                    m_Animations.GetObject(pSync.Id).SetTrigger(pSync.State.ToString());
                }
            })
            .WithoutBurst()
            .Run();
        return inputDeps;
    }
}
