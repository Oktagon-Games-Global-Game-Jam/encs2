using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct C_SpawnRequest : IComponentData
{
    public Entity Reference;
    public float3 Position; // Start Position At World
    public byte Direction; // -1 left / 1 right
}
