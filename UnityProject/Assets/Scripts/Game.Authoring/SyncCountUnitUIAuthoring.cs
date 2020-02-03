using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SyncCountUnitUIAuthoring  : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private HUDView m_Reference;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        World.AllWorlds[0].GetOrCreateSystem<ECSCountUnitUISync>().m_HudView = new ECSComponentMono<HUDView>();
        World.AllWorlds[0].GetOrCreateSystem<ECSCountUnitUISync>().m_HudView.AddObject(m_Reference.GetInstanceID(), m_Reference);
        
        dstManager.AddComponent(entity, typeof(C_SyncCountUnitUI));
        dstManager.SetComponentData(entity, new C_SyncCountUnitUI
        {
            Id = m_Reference.GetInstanceID(),
     });
    }
}
