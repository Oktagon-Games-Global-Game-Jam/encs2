using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SyncPositionAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private Transform m_ReferenceTransform;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if(World.AllWorlds[0].GetExistingSystem<ECSPositionSync>().m_Transforms == null)
        {
            World.AllWorlds[0].GetExistingSystem<ECSPositionSync>().m_Transforms = new ECSComponentMono<Transform>();
        }
        World.AllWorlds[0].GetExistingSystem<ECSPositionSync>().m_Transforms.AddObject(m_ReferenceTransform.GetInstanceID(), m_ReferenceTransform);
        dstManager.AddComponent(entity, typeof(C_SyncPositionMono));
        dstManager.SetComponentData(entity, new C_SyncPositionMono
        {
            Id = m_ReferenceTransform.GetInstanceID(),
            Position = m_ReferenceTransform.position
        });
    }
}
