using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

[UpdateBefore(typeof(S_Die))]
public class S_EnemyUnitGetInTargetResolver : JobComponentSystem
{
    public EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    public struct KamikazeJob : IJobForEachWithEntity<T_EnemyUnit, C_ReachTarget, Translation, C_MechaPart>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly]ref T_EnemyUnit tEnemyUnit, [ReadOnly]ref C_ReachTarget cReachTarget, [ReadOnly]ref Translation pTranslation, [ReadOnly]ref C_MechaPart cMechaPart)
        {
            if (Math.DistanceXZ(cReachTarget.TargetPosition, pTranslation.Value) <= 1f)
            {
                eEntityCommandBuffer.AddComponent(index, entity, new C_DamageToTake{DamageToTake = 1});
                Entity damageMechaEntity = eEntityCommandBuffer.CreateEntity(index);
                eEntityCommandBuffer.AddComponent(index, damageMechaEntity, new C_DamageMecha{DamageToDeal = 1});
                eEntityCommandBuffer.AddComponent(index, damageMechaEntity, new C_MechaPart{MechaPart = cMechaPart.MechaPart});
            }
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new KamikazeJob
        {
            eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent(),
        }.Schedule(this, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}
