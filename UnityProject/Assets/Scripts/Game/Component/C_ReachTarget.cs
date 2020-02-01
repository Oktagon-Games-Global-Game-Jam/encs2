using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct C_ReachTarget : IComponentData
{
    public float3 TargetPosition;
    public float ReachDistance;
    public E_MechaPart MechaPart;
}


public enum E_MechaPart
{
    Head = 0,
    Body,
    Legs,
}