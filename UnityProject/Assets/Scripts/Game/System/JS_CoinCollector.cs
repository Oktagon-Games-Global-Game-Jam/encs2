using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Rendering;

[UpdateAfter(typeof(S_Die))]
class JS_CoinCollector : ComponentSystem
{
    private float m_Cooldown;


    protected override void OnUpdate()
    {
        float fTtime = Time.DeltaTime;
        m_Cooldown -= fTtime;
        if (m_Cooldown <= 0)
        {
            Unity.Mathematics.Random pRand = new Unity.Mathematics.Random(324);
            m_Cooldown = 0f;
            Entities.ForEach(
                (Entity entity, ref C_Resource pRes, ref C_Rotate pRotate) =>
                {
                    pRotate.Speed *= 5f;
                    EntityManager.AddComponentData(entity, new C_AnimateToResource() { Value = 0, Wait = pRand.NextFloat(4f, 5f) });
                    EntityManager.RemoveComponent(entity, typeof(C_Resource));
                    })
            ;
        }
    }
}