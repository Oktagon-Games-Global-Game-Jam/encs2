using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UnitAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [Header("Move")]
    public float m_MoveSpeed = .1f;
    [Header("Reach Taret")]
    public Vector3 TargetPosition;
    public float ReachDistance;
    public E_MechaPart MechaPart;
    public float ActionDistance;
    [Header("Life")]
    public int MaxLife;
    public int ActualLife;
    public int modifyLifeValue;
    
    [Header("Spawn")]
    public bool IsEnemy;
    public float Cooldown;
    public int SpawnAmount;
    public float ReduceByTime;
    public float4 SpawnArea;
    
    public virtual void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new C_Move() { Speed = m_MoveSpeed });
        dstManager.AddComponentData(entity, new C_ReachTarget() { ReachDistance = ReachDistance, TargetPosition = TargetPosition, ActionDistance = ActionDistance});
        dstManager.AddComponentData(entity, new C_MechaPart() { MechaPart = MechaPart});
        dstManager.AddComponentData(entity, new C_Life() { MaxLife = MaxLife, ActualLife = ActualLife });
        dstManager.AddComponentData(entity, new C_Unit{ModifyLifeValue = modifyLifeValue});
        dstManager.AddComponentData(entity, new C_SpawnData{ Cooldown = Cooldown, MechaLane = MechaPart, SpawnAmount = SpawnAmount, TimeCache = 0, ReduceTimeBySecond = ReduceByTime, SpawnArea = SpawnArea});
        dstManager.AddComponentData(entity, new Prefab());
        if (IsEnemy)
            dstManager.AddComponentData(entity, new T_Enemy());
        else
            dstManager.AddComponentData(entity, new T_Ally());
    }
}
