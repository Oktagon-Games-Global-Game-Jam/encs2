using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

public class MechaAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [Header("Move")]
    public float m_MoveSpeed = 0.01f;

    [Header("Part")] public E_MechaPart m_EMechaPart;
    [Header("Life")] public int life;

    public virtual void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new C_Move() { Speed = m_MoveSpeed });
        dstManager.AddComponentData(entity, new C_MechaPart { MechaPart = m_EMechaPart});
        dstManager.AddComponent<T_Mecha>(entity);
        dstManager.AddComponentData(entity, new C_Life {MaxLife = life, ActualLife = life});

    }
}
