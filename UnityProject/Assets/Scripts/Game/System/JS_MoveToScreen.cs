using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Rendering;

public class JS_MoveToScreen : JobComponentSystem
{

    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    float3 m_pTarget = float3.zero;
    Transform m_Transform;

    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        
    }
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        m_Transform = GameObject.Find("CoinCollector").transform;
        //m_pTarget = GameObject.Find("CoinCollector").transform.position;
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float fTtime = Time.DeltaTime;
        var tCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
        float3 pTarget = m_Transform.position;// m_pTarget;
        return 
        Entities
            .ForEach((int entityInQueryIndex, Entity pEntity, ref C_AnimateToResource pAnimate, ref Translation pTranslation) =>
            {
                pAnimate.Wait -= fTtime;
                if (pAnimate.Wait <= 0)
                {
                    pTranslation.Value += (fTtime * 40f * math.normalize(pTarget - pTranslation.Value));
                    if (math.distance(pTarget, pTranslation.Value) <= 1f)
                    {
                        //AudioManager.Play(AudioManager.SoundList.AddCoin, false);
                        tCommandBuffer.DestroyEntity(entityInQueryIndex, pEntity);
                    }
                }
            })
            .Schedule(inputDeps);
    }
}