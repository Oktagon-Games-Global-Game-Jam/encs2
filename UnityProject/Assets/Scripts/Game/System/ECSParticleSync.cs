using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
public class ECSParticleSync : JobComponentSystem
{
    public ECSComponentMono<ParticleSystemController> m_ParticleSystemControllers;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((in C_Particle pSync) =>
            {
               
                m_ParticleSystemControllers.GetObject(pSync.id).SpawnParticle(pSync);
            })
            .WithoutBurst()
            .Run();
        return inputDeps;
    }
}
