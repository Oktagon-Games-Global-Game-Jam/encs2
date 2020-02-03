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
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    private float m_Cooldown;
    private bool m_CollectingPhase;

    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        float fTtime = Time.DeltaTime;
        m_Cooldown -= fTtime;
        if (m_Cooldown <= 0)
        {
            Unity.Mathematics.Random pRand = new Unity.Mathematics.Random(324);
            m_Cooldown = 0f;
            var tCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
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