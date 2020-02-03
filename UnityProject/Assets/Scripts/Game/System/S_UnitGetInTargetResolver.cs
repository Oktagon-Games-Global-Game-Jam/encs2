using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

[UpdateBefore(typeof(S_Die))]
public class S_UnitGetInTargetResolver : JobComponentSystem
{
    public EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    public EntityQuery removeDeadEntities;
    protected override void OnCreate()
    {
        m_EndSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        removeDeadEntities = GetEntityQuery(typeof(C_Unit), ComponentType.Exclude<T_IsDead>());
    }

    public struct KamikazeJob : IJobForEachWithEntity<C_Unit, C_ReachTarget, Translation, C_MechaPart>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly]ref C_Unit cUnit, [ReadOnly]ref C_ReachTarget cReachTarget, [ReadOnly]ref Translation pTranslation, [ReadOnly]ref C_MechaPart cMechaPart)
        {
            if (Math.DistanceXZ(cReachTarget.TargetPosition, pTranslation.Value) <= cReachTarget.ActionDistance)
            {
                eEntityCommandBuffer.AddComponent(index, entity, new C_DamageToTake{DamageToTake = 1});
                Entity damageMechaEntity = eEntityCommandBuffer.CreateEntity(index);
                eEntityCommandBuffer.AddComponent(index, damageMechaEntity, new C_ModifyLife{modifyValue = cUnit.ModifyLifeValue});
                eEntityCommandBuffer.AddComponent(index, damageMechaEntity, new C_MechaPart{MechaPart = cMechaPart.MechaPart});
            }
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (m_EndSimulationEntityCommandBufferSystem == null)
            m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();

        JobHandle jobHandle = new KamikazeJob
        {
            eEntityCommandBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent(),
        }.Schedule(this, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }
}
