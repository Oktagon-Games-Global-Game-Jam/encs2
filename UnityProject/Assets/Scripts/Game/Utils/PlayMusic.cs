using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public bool Loop;
    public AudioManager.SoundList m_SoundList;
    public AudioManager.SoundList[] m_SoundStop;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_SoundStop.Length; i++)
        {
            AudioManager.Stop(m_SoundStop[i]);
        }
        AudioManager.Play(m_SoundList, Loop);
    }

}
