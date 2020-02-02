using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
[UpdateBefore(typeof(S_Die))]
public class S_EndGame : ComponentSystem
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
    
    
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref C_Move move) =>
        {
            EntityManager.RemoveComponent(entity, typeof(C_Move));
        });
        

        Entities.ForEach((Entity entity, ref C_EndGame tGameWon) =>
        {
            if (tGameWon.IsMechaWinner)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(k.Scenes.UI_Victory,
                    UnityEngine.SceneManagement.LoadSceneMode.Additive);
                Debug.Log("GameWon");
                
                Entities.ForEach((Entity pEntity, ref C_SyncAnimationMono Sync) =>
                {
                    Sync.State = E_State.Win; 
                });
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(k.Scenes.UI_Defeat,
                    UnityEngine.SceneManagement.LoadSceneMode.Additive);
                Debug.Log("GameLost");
                Entities.ForEach((Entity pEntity, ref C_SyncAnimationMono Sync) =>
                {
                    Sync.State = E_State.Defeat; 
                });
            }
            EntityManager.DestroyEntity(entity);
        });


    }

    //protected override void OnUpdate()
    //{
    //    Entities.ForEach()
    //}
}
