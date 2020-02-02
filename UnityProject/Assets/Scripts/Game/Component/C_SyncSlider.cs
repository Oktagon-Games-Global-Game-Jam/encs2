using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct C_SyncSlider  : IComponentData
{
    public int Id;
    public float Value;
}