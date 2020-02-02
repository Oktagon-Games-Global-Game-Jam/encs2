using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class S_CountUnit : JobComponentSystem
{
    private EntityQuery m_PlayerUnitQuery;
    private EntityQuery m_EnemyUnitQuery;
    private EntityQuery m_DeadQuery;
    private int m_Kills = 0;
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
        
        m_DeadQuery = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadOnly<C_Unit>(),
            ComponentType.ReadOnly<T_Ally>(), 
            ComponentType.ReadOnly<T_IsDead>(), 
        });
        
        m_DeadQuery.AddChangedVersionFilter(ComponentType.ReadOnly<T_IsDead>());
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        int tPlayerUnits = m_PlayerUnitQuery.CalculateEntityCount();
        int tEnemyUnits = m_EnemyUnitQuery.CalculateEntityCount();
        m_Kills += m_DeadQuery.CalculateEntityCount();
        int tKills = m_Kills;
        
        return Entities.ForEach((ref C_SyncCountUnitUI SyncCountUnitUi) =>
        {
            SyncCountUnitUi.EnemyCount = tEnemyUnits;
            SyncCountUnitUi.PlayerCount = tPlayerUnits;
            SyncCountUnitUi.KillCount = tKills;
        }).WithoutBurst().Schedule(inputDeps);
    }
}
