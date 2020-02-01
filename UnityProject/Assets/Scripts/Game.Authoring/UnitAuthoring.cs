using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class UnitAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [Header("Move")]
    public float m_MoveSpeed = .1f;
    [Header("Reach Taret")]
    public Vector3 TargetPosition;
    public float ReachDistance;
    public E_MechaPart MechaPart;
    [Header("Life")]
    public int MaxLife;
    public int ActualLife;
    public int modifyLifeValue;


    public virtual void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new C_Move() { Speed = m_MoveSpeed });
        dstManager.AddComponentData(entity, new C_ReachTarget() { ReachDistance = ReachDistance, TargetPosition = TargetPosition });
        dstManager.AddComponentData(entity, new C_MechaPart() { MechaPart = MechaPart});
        dstManager.AddComponentData(entity, new C_Life() { MaxLife = MaxLife, ActualLife = ActualLife });
        dstManager.AddComponentData(entity, new Prefab());
        dstManager.AddComponentData(entity, new C_Unit{ModifyLifeValue = modifyLifeValue});
    }
}
