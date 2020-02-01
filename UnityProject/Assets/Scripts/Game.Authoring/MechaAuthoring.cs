using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

public class MechaAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [Header("Move")]
    public float m_MoveSpeed = .1f;
    //[Header("MechaParts")]
    //public GameObject Head;
    //public GameObject Body;
    //public GameObject Legs;

    public virtual void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new C_Move() { Speed = m_MoveSpeed });

    }
}
