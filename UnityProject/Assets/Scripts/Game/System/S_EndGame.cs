using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
[UpdateBefore(typeof(S_Die))]
public class S_EndGame : ComponentSystem
{

    protected override void OnUpdate()
    {
        /*
        Entities.ForEach((Entity entity, ref C_Move move) =>
        {
            EntityManager.RemoveComponent(entity, typeof(C_Move));
        });*/
        
        Entities.ForEach((Entity entity, ref C_EndGame tGameWon) =>
        {
            if (tGameWon.IsMechaWinner)
            {
                if (!IsOpen(k.Scenes.UI_Victory))
                {
                    var pScene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(k.Scenes.UI_Victory);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(k.Scenes.UI_Victory,
                        UnityEngine.SceneManagement.LoadSceneMode.Additive);
                    Debug.Log("GameWon");

                    Entities.ForEach((Entity pEntity, ref C_SyncAnimationMono Sync) =>
                    {
                        Sync.State = E_State.Win;
                    });
                }
            }
            else
            {
                if (!IsOpen(k.Scenes.UI_Defeat))
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(k.Scenes.UI_Defeat,
                    UnityEngine.SceneManagement.LoadSceneMode.Additive);
                    Debug.Log("GameLost");
                    Entities.ForEach((Entity pEntity, ref C_SyncAnimationMono Sync) =>
                    {
                        Sync.State = E_State.Defeat;
                    });
                }
            }
            EntityManager.DestroyEntity(entity);
        });
    }

    bool IsOpen(int i)
    {
        return false;
        for (int iCount = 0; iCount < UnityEngine.SceneManagement.SceneManager.sceneCount; iCount++)
        {
            var pScene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(iCount);
            if (pScene.buildIndex == i)
                return true;
        }
        return false;
    }
}
