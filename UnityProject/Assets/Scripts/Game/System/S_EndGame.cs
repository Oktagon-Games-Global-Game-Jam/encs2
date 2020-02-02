using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
[UpdateBefore(typeof(S_Die))]
public class S_EndGame : JobComponentSystem
{
    public EndSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    private EntityQuery GetEntityOnChangeVersion;
    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
         GetEntityOnChangeVersion = GetEntityQuery(new ComponentType[]
        {
            ComponentType.ReadOnly<C_EndGame>(), 
            ComponentType.Exclude<T_GameEnd>() 
        });
    }

    public struct EndGameJob: IJobForEachWithEntity<C_EndGame>
    {
        public EntityCommandBuffer.Concurrent eEntityCommandBuffer;
        

        public void Execute(Entity entity, int index, [ReadOnly]ref C_EndGame tGameWon)
        {
            eEntityCommandBuffer.AddComponent<T_GameEnd>(index, entity);
            if (tGameWon.IsMechaWinner)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(k.Scenes.UI_Victory, UnityEngine.SceneManagement.LoadSceneMode.Additive);
                Debug.Log("GameWon");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(k.Scenes.UI_Defeat, UnityEngine.SceneManagement.LoadSceneMode.Additive);
                Debug.Log("GameLost");
            }

        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new EndGameJob
        {
            eEntityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent(),
        }
        .Run(GetEntityOnChangeVersion, inputDeps);
        //.Schedule(GetEntityOnChangeVersion, inputDeps);
        jobHandle.Complete();
        return jobHandle;
    }

    //protected override void OnUpdate()
    //{
    //    Entities.ForEach()
    //}
}
