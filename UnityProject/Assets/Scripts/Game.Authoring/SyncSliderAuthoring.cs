using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class SyncSliderAuthoring  : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private Slider m_Reference;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if( World.AllWorlds[0].GetOrCreateSystem<ECSSliderSync>().m_Sliders == null)
            World.AllWorlds[0].GetOrCreateSystem<ECSSliderSync>().m_Sliders = new ECSComponentMono<Slider>();
        World.AllWorlds[0].GetOrCreateSystem<ECSSliderSync>().m_Sliders.AddObject(m_Reference.GetInstanceID(), m_Reference);
        
        dstManager.AddComponent(entity, typeof(C_SyncSlider));
        dstManager.SetComponentData(entity, new C_SyncSlider
        {
            Id = m_Reference.GetInstanceID(),
            Value = 0
        });
    }
}
