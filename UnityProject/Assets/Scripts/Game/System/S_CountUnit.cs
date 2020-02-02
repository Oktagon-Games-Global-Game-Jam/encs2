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

        m_KillQuery.AddChangedVersionFilter(ComponentType.ReadOnly<T_IsDead>());
        
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        int tPlayerUnits = m_PlayerUnitQuery.CalculateEntityCount();
        int tEnemyUnits = m_EnemyUnitQuery.CalculateEntityCount();
        int Resources = World.GetExistingSystem<S_CreditResourceOnRetrive>().AmountResource;
        m_Kills += m_KillQuery.CalculateEntityCount();
        
        int tKills = m_Kills;


        return Entities.ForEach((ref C_SyncCountUnitUI SyncCountUnitUi) =>
        {
            SyncCountUnitUi.EnemyCount = tEnemyUnits;
            SyncCountUnitUi.PlayerCount = tPlayerUnits;
            SyncCountUnitUi.KillCount = tKills;
            SyncCountUnitUi.Resource = Resources;
        }).WithoutBurst().Schedule(inputDeps);
    }
}
