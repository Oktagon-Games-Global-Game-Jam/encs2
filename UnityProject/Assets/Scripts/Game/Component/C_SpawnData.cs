using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct C_SpawnData : IComponentData
{
    public float Cooldown;
    public int SpawnAmount;
    public E_MechaPart MechaLane;
    public bool IsEnemy;
    public float ReduceTimeBySecond;

    [HideInInspector] public float TimeCache;
}
