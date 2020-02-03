using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class S_CreditResourceOnRetrive : ComponentSystem
{
    private EntityQuery m_Query;
    public int AmountResource = 0;
    protected override void OnCreate()
    {
        base.OnCreate();
        m_Query = GetEntityQuery(new ComponentType[]
        {    
            ComponentType.ReadOnly<C_Resource>(), 
        });
        AmountResource = 150;
        m_Query.SetChangedVersionFilter(ComponentType.ReadOnly<C_Resource>());
    }

    protected override void OnUpdate()
    {
        AmountResource += m_Query.CalculateChunkCount() * 2;
    }
}