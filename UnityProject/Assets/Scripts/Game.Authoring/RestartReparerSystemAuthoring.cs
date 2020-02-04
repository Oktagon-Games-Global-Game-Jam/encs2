using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RestartReparerSystemAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public int m_StartAmountResource;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        World.AllWorlds[0].GetExistingSystem<S_CreditResourceOnRetrive>().AmountResource = m_StartAmountResource;
;    }
}
