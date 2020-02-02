using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct C_Particle : IComponentData
{
    public int id;
    public float3 ParticlePos;
    public int Count;
}
