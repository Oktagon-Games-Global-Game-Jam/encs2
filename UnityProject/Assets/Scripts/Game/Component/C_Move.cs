﻿using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct C_Move : IComponentData
{
    public float3 Speed;
}


public struct C_TargetReached : IComponentData
{

}

public struct T_Target: IComponentData { }


