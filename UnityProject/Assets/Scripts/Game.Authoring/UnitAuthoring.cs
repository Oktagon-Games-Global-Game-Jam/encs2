using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class UnitAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] public ParticleSystemController reference;
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
    public float3 SpawnOffset;
    
    public virtual void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new C_Move() { Speed = m_MoveSpeed });
        dstManager.AddComponentData(entity, new C_ReachTarget() { ReachDistance = ReachDistance, TargetPosition = TargetPosition, ActionDistance = ActionDistance});
        dstManager.AddComponentData(entity, new C_MechaPart() { MechaPart = MechaPart});
        dstManager.AddComponentData(entity, new C_Life() { MaxLife = MaxLife, ActualLife = ActualLife });
        if( World.AllWorlds[0].GetOrCreateSystem<ECSParticleSync>().m_ParticleSystemControllers == null)
            World.AllWorlds[0].GetOrCreateSystem<ECSParticleSync>().m_ParticleSystemControllers = new ECSComponentMono<ParticleSystemController>();
        World.AllWorlds[0].GetOrCreateSystem<ECSParticleSync>().m_ParticleSystemControllers.AddObject(reference.GetInstanceID(), reference);
        dstManager.AddComponentData(entity, new C_Unit{ModifyLifeValue = modifyLifeValue, ParticleSystemReference = reference.GetInstanceID()});
        dstManager.AddComponentData(entity, new C_SpawnData{ Cooldown = Cooldown, MechaLane = MechaPart, SpawnAmount = SpawnAmount, TimeCache = 0, ReduceTimeBySecond = ReduceByTime, SpawnArea = SpawnArea, Offset = SpawnOffset});
        dstManager.AddComponentData(entity, new Prefab());
        if (IsEnemy)
            dstManager.AddComponentData(entity, new T_Enemy());
        else
            dstManager.AddComponentData(entity, new T_Ally());
    }
}
