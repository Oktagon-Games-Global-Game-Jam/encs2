using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class ECSCountUnitUISync : JobComponentSystem
{
    public ECSComponentMono<HUDView> m_HudView;
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        m_HudView = new ECSComponentMono<HUDView>(Object.FindObjectsOfType<HUDView>());    
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((in C_SyncCountUnitUI pSync) =>
            {
                m_HudView.GetObject(pSync.Id).SetEnemyUnitAmount(pSync.EnemyCount);
                m_HudView.GetObject(pSync.Id).SetPlayerUnitAmount(pSync.PlayerCount);
            })
            .WithoutBurst()
            .Run();
        return inputDeps;
    }
}
