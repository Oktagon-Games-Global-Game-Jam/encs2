using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct C_ReachTarget : IComponentData
{
    public float3 TargetPosition;
    public float ReachDistance;
}


public enum E_TargetType
{
    MechaPart,
    Mouse
}

public enum E_MechaPart
{
    Head = 0,
    Body,
    Legs,
}