using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SyncAnimationAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private Animator m_Reference;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if( World.AllWorlds[0].GetOrCreateSystem<ECSAnimationSync>().m_Animations == null)
            World.AllWorlds[0].GetOrCreateSystem<ECSAnimationSync>().m_Animations = new ECSComponentMono<Animator>();
        World.AllWorlds[0].GetOrCreateSystem<ECSAnimationSync>().m_Animations.AddObject(m_Reference.GetInstanceID(), m_Reference);
        
        dstManager.AddComponent(entity, typeof(C_SyncAnimationMono));
        dstManager.SetComponentData(entity, new C_SyncAnimationMono
        {
            Id = m_Reference.GetInstanceID(),
            State = E_State.Walking
        });
    }
}
