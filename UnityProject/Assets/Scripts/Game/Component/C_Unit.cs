using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct C_Unit : IComponentData
{
    public int ModifyLifeValue;
    public int ParticleSystemReference;
}
