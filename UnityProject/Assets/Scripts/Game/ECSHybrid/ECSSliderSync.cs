using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class ECSSliderSync : JobComponentSystem
{
    public ECSComponentMono<Slider> m_Sliders;
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        m_Sliders = new ECSComponentMono<Slider>();    
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((in C_SyncSlider pSync) =>
            {
                m_Sliders.GetObject(pSync.Id).value = pSync.Value;
            })
            .WithoutBurst()
            .Run();
        return inputDeps;
    }
}
