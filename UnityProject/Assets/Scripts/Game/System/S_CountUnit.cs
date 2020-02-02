using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class S_CountUnit : JobComponentSystem
{
    private EntityQuery m_PlayerUnitQuery;
    private EntityQuery m_EnemyUnitQuery;
    private EntityQuery m_KillQuery;
    private EntityQuery m_ScoreQuery;
    private int m_Kills = 0;
    private int m_Resources = 0;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_EnemyUnitQuery = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadOnly<C_Unit>(),
            ComponentType.ReadOnly<T_Enemy>(), 
        });
        
        m_PlayerUnitQuery = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadOnly<C_Unit>(),
            ComponentType.ReadOnly<T_Ally>(), 
        });
        
        m_KillQuery = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadOnly<C_Unit>(),
            ComponentType.ReadOnly<T_Enemy>(), 
            ComponentType.ReadOnly<T_IsDead>(), 
        });
        m_ScoreQuery = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadOnly<T_Enemy>(),
            ComponentType.ReadOnly<C_AnimateToResource>(),
        });

        m_KillQuery.AddChangedVersionFilter(ComponentType.ReadOnly<T_IsDead>());
        m_ScoreQuery.AddChangedVersionFilter(ComponentType.ReadOnly<C_AnimateToResource>());
        
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        int tPlayerUnits = m_PlayerUnitQuery.CalculateEntityCount();
        int tEnemyUnits = m_EnemyUnitQuery.CalculateEntityCount();
        m_Kills += m_KillQuery.CalculateEntityCount();
        m_Resources += m_ScoreQuery.CalculateEntityCount();
        int tKills = m_Kills;
        int tResource = m_Resources;

        return Entities.ForEach((ref C_SyncCountUnitUI SyncCountUnitUi) =>
        {
            SyncCountUnitUi.EnemyCount = tEnemyUnits;
            SyncCountUnitUi.PlayerCount = tPlayerUnits;
            SyncCountUnitUi.KillCount = tKills;
            SyncCountUnitUi.Resource = tResource;
        }).WithoutBurst().Schedule(inputDeps);
    }
}
