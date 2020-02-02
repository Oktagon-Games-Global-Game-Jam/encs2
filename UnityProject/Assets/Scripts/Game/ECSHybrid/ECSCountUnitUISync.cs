using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class ECSCountUnitUISync : ComponentSystem
{
    public ECSComponentMono<HUDView> m_HudView;
    
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((ref C_SyncCountUnitUI pSync) =>
        {
            m_HudView.GetObject(pSync.Id).SetEnemyUnitAmount(pSync.EnemyCount);
            m_HudView.GetObject(pSync.Id).SetPlayerUnitAmount(pSync.PlayerCount);
            m_HudView.GetObject(pSync.Id).SetKillAmount(pSync.KillCount);
            m_HudView.GetObject(pSync.Id).SetAmount(0, pSync.Resource);
            m_HudView.GetObject(pSync.Id).SetAmount(1, pSync.Resource);
            m_HudView.GetObject(pSync.Id).SetAmount(2, pSync.Resource);
        });

    }
}
