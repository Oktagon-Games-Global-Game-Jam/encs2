using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct C_SyncCountUnitUI : IComponentData
{
    public int Id;
    public int EnemyCount;
    public int PlayerCount;
    public int KillCount;
}
