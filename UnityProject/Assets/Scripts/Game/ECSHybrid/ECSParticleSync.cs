using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
public class ECSParticleSync : JobComponentSystem
{
    public ECSComponentMono<ParticleSystemController> m_ParticleSystemControllers;
    public EndSimulationEntityCommandBufferSystem eEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        eEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
            
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (eEntityCommandBufferSystem == null)
            eEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();

        var pBuffer = eEntityCommandBufferSystem.CreateCommandBuffer();
        Entities.ForEach((int entityInQueryIndex, in Entity entity, in C_Particle pSync) =>
            {
                m_ParticleSystemControllers.GetObject(pSync.id).SpawnParticle(pSync);
                pBuffer.RemoveComponent<C_Particle>(entity);
                
            })
            .WithoutBurst()
            .Run();
        return inputDeps;
    }
}
