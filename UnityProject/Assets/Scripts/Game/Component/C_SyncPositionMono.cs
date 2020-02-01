using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class C_SyncPositionMono : IComponentData
{
    public float3 Position;
    public int Id;
}
