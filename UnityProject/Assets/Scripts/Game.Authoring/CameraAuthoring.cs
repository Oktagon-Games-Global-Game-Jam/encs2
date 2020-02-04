using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CameraAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent(entity, typeof(C_SyncPositionMono));

        dstManager.AddComponent(entity, typeof(T_CameraFollowData));
        dstManager.AddComponent(entity, typeof(T_Camera));
        dstManager.AddComponent(entity, typeof(C_ReachTarget));
        dstManager.SetComponentData(entity, new C_ReachTarget
        {
            TargetPosition = new float3(-30, 8, 0)
        });
    }
}
