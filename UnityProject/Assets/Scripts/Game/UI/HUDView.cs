﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour
{
    
    //[Serializable]
    //public class SpawnButton
    //{
    //    public Text m_TextAmount;
    //    public Image m_Texture;
    //    public Button m_Button;
    //}

    [SerializeField] private Text m_TextKillCount;
    [SerializeField] private SpawnButton m_ButtonSpawnTemplate;
    [SerializeField] private SpawnButton[] m_Buttons;

    public void Setup()
    {

    }


    #region core methods
    public void SetKillAmount(int iAmount)
    {
        m_TextKillCount.text = iAmount.ToString();
    }
    public void SetOnClick(int iIdx, System.Action pOnClick)
    {
        m_Buttons[iIdx].m_Button.onClick.AddListener(() => pOnClick());
    }
    public void SetEnabled(int iIdx, bool bEnabled)
    {
        m_Buttons[iIdx].m_Button.interactable = bEnabled;
    }
    public void SetAmount(int iIdx, int iAmount)
    {
        m_Buttons[iIdx].m_TextAmount.text = iAmount.ToString();
    }
    #endregion
}
