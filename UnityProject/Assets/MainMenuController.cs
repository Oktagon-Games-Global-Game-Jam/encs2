using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Sprite muteSoundSprite;
    [SerializeField] private Sprite soundEnabledSprite;
    [SerializeField] private Image soundImage;
    private bool m_IsSoundMuted = false;

    public void ToggleMuteSound()
    {
        m_IsSoundMuted = !m_IsSoundMuted;
        if (m_IsSoundMuted)
        {
            soundImage.sprite = muteSoundSprite;
        }
        else
            soundImage.sprite = soundEnabledSprite;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StarGame()
    {
        SceneManager.LoadScene("Scenes/Game",LoadSceneMode.Single);
    }

}
