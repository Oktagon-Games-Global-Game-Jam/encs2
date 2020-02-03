using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public int m_SceneIndex;
    public UnityEngine.SceneManagement.LoadSceneMode m_LoadMode = UnityEngine.SceneManagement.LoadSceneMode.Additive;
    // Start is called before the first frame update
    void Start()
    {
        if(m_LoadMode == UnityEngine.SceneManagement.LoadSceneMode.Single)
        {
            var entityManager = World.AllWorlds[0].EntityManager;
            foreach (var e in entityManager.GetAllEntities())
                entityManager.DestroyEntity(e);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(m_SceneIndex, m_LoadMode);
    }
}

