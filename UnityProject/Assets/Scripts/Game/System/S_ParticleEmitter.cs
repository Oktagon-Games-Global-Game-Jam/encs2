using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
/*
public class S_ParticleEmitter : JobComponentSystem
{
    private ParticleSystemController _particleSystemController;
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnStartRunning()
    {
        _particleSystemController = Object.FindObjectOfType<ParticleSystemController>();
    }

    public struct ParticleEmmiterJob: IJobForEachWithEntity<C_Particle>
    {
        public NativeList<C_Particle> cParticles;
        public EntityCommandBuffer.Concurrent cConcurrent;
        public void Execute(Entity entity, int index, ref C_Particle cParticle)
        {
            cParticles.Add(cParticle);
            cConcurrent.RemoveComponent<C_Particle>(index, entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        NativeList<C_Particle> cParticles = new NativeList<C_Particle>();
        JobHandle jobHandle = new ParticleEmmiterJob
        {
            cParticles = cParticles, cConcurrent = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
        }.Schedule(this, inputDeps);
        _particleSystemController.SpawnParticle(cParticles[0]);
        jobHandle.Complete();
        cParticles.Dispose();
        return jobHandle;
    }
}
*/