using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioReference
    {
        public SoundList m_SoundList;
        public AudioSource m_AudioSource;
    }

    [SerializeField]
    public AudioReference[] m_AudioReference;


    static AudioManager Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    AudioReference GetSound(SoundList eSoundList)
    {
        for (int i = 0; i < m_AudioReference.Length; i++)
        {
            if(m_AudioReference[i].m_SoundList == eSoundList)
            {
                return m_AudioReference[i];
            }
        }
        return null;
    }
    public static void PlayOneShot(SoundList eSound, bool bLoop)
    {
        if (Instance == null)
            return;
        var pSound = Instance.GetSound(eSound);
        if (pSound == null || pSound.m_AudioSource == null)
            return;
        pSound.m_AudioSource.loop = bLoop;
        pSound.m_AudioSource.pitch = Random.Range(.95f, 1.05f);
        pSound.m_AudioSource.PlayOneShot(pSound.m_AudioSource.clip);
    }
    public static void Play(SoundList eSound, bool bLoop)
    {
        if (Instance == null)
            return;
        var pSound = Instance.GetSound(eSound);
        if (pSound == null || pSound.m_AudioSource == null)
            return;
        pSound.m_AudioSource.loop = bLoop;
        if(!bLoop)
            pSound.m_AudioSource.pitch = Random.Range(.95f, 1.05f);
        pSound.m_AudioSource.Play();
    }
    public static void Stop(SoundList eSound)
    {
        if (Instance == null)
            return;
        var pSound = Instance.GetSound(eSound);
        if (pSound == null || pSound.m_AudioSource == null)
            return;
        pSound.m_AudioSource.Stop();
    }

    public enum SoundList
    {
        MusicMenu = 1,
        MusicBattle,

        SpawnUnit1 = 100,
        SpawnUnit2,
        SpawnUnit3,
        SpawnEnemy,
        AddCoin,

        FootStep = 200,


        Victory = 300,
        Defeat,
        Start,
    }
}
