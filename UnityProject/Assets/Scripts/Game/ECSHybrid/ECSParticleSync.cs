using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
public class ECSParticleSync : JobComponentSystem
{
    public ECSComponentMono<ParticleSystemController> m_ParticleSystemControllers;
    public BeginSimulationEntityCommandBufferSystem eEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        eEntityCommandBufferSystem = World.GetExistingSystem<BeginSimulationEntityCommandBufferSystem>();
            
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (eEntityCommandBufferSystem == null)
            eEntityCommandBufferSystem = World.GetExistingSystem<BeginSimulationEntityCommandBufferSystem>();

        var pBuffer = eEntityCommandBufferSystem.CreateCommandBuffer();
        Entities.ForEach((int entityInQueryIndex, in Entity entity, in C_Particle cParticle, in Translation pTranslation) =>
            {
                m_ParticleSystemControllers.GetObject(cParticle.id).SpawnParticle(new C_Particle{ParticlePos = pTranslation.Value, Count = cParticle.Count});
                pBuffer.RemoveComponent<C_Particle>(entity);
                
            })
            .WithoutBurst()
            .Run();
        return inputDeps;
    }
}
