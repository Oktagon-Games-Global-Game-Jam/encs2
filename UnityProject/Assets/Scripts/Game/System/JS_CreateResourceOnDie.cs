using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Rendering;

[UpdateAfter(typeof(S_Die))]
class JS_CreateResourceOnDie : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EndSimulationEntityCommandBufferSystem;
    static private SC_ResourceMesh m_ResourceMesh;
    private Rotation m_RotationTemplate;
    private Scale m_ScaleTemplate;
    private EntityQuery m_KillQuery;

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        var pGO = GameObject.Find("CoinTemplate");
        m_ResourceMesh = new SC_ResourceMesh()
        {
            material = pGO.GetComponent<MeshRenderer>().sharedMaterial,
            mesh = pGO.GetComponent<MeshFilter>().sharedMesh
        };
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        m_EndSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        m_KillQuery = GetEntityQuery(new ComponentType[]
        {
                    ComponentType.ReadOnly<C_Unit>(),
                    ComponentType.ReadOnly<T_Enemy>(),
                    ComponentType.ReadOnly<T_IsDead>(),
        });

        m_KillQuery.AddChangedVersionFilter(ComponentType.ReadOnly<T_IsDead>());
    }
    


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var pBuffer = m_EndSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
        var pTask =
            Entities
            .WithoutBurst()
            .ForEach((int entityInQueryIndex, in T_IsDead tIsDead, in Translation pTranslation, in T_Enemy pEnemy) =>
                {
                    var pEntity = pBuffer.CreateEntity(entityInQueryIndex);
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new Translation() { Value = pTranslation.Value });
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new C_Resource() { Value = 1 });
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new LocalToWorld());
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new Rotation() { Value = quaternion.AxisAngle(new float3(1f, 0f, 0f), -90f) });
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new NonUniformScale() { Value = new float3(.6f, .1f, .6f)  });
                    pBuffer.AddComponent(entityInQueryIndex, pEntity, new C_Rotate() { Speed = 60f });
                    pBuffer.AddSharedComponent(entityInQueryIndex, pEntity, new RenderMesh() { mesh = m_ResourceMesh.mesh, material = m_ResourceMesh.material });
                })
            .Schedule(m_KillQuery.GetDependency());
        pTask.Complete();
        return pTask;
    }
}