using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public int m_SceneIndex;
    public UnityEngine.SceneManagement.LoadSceneMode m_LoadMode = UnityEngine.SceneManagement.LoadSceneMode.Additive;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(m_SceneIndex, m_LoadMode);
    }
}
