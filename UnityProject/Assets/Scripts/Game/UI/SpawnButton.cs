using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Text m_TextAmount;
    public Image m_Texture;
    public Button m_Button;
    public AudioManager.SoundList m_ClickDown;
    public AudioManager.SoundList m_ClickUp;
    private System.Action m_PointerDown, m_PointerUp;
    

    public void Setup(System.Action PointerDown, System.Action PointerUp)
    {
        m_PointerDown = PointerDown;
        m_PointerUp = PointerUp;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_PointerDown?.Invoke();
        AudioManager.Play(m_ClickDown, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_PointerUp?.Invoke();
        AudioManager.Stop(m_ClickDown);
    }
    public void SetAmount(int iAmount)
    {
        int iOld = m_Amount;
        int iNew = iAmount;
        m_Amount = iAmount;
        m_TextAmount.text = iAmount.ToString();
        if (iNew - iOld > 1)
            AudioManager.PlayOneShot(AudioManager.SoundList.AddCoin, false);
    }
    private int m_Amount;

}


