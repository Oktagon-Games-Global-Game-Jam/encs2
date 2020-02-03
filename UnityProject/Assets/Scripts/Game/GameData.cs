using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public LevelData m_LevelData;

    public void OnDrawGizmos()
    {
        if (m_LevelData)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(m_LevelData.m_EnemySpawnPointX, 0, 0), 2);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(m_LevelData.m_PlayerSpawnPointX, 0, 0), 2);
        }
    }
}

