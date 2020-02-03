using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class ECSSliderSync : ComponentSystem
{
    public ECSComponentMono<Slider> m_Sliders;
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((ref C_SyncSlider pSync) => { m_Sliders.GetObject(pSync.Id).value = pSync.Value; });
    }
}
