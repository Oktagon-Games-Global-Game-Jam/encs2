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

    private System.Action m_PointerDown, m_PointerUp;

    public void Setup(System.Action PointerDown, System.Action PointerUp)
    {
        m_PointerDown = PointerDown;
        m_PointerUp = PointerUp;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_PointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_PointerUp?.Invoke();
    }
}

