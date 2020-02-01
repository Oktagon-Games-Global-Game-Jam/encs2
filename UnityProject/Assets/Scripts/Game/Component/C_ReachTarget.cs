using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct C_ReachTarget : IComponentData
{
    public float3 TargetPosition;
    public float ReachDistance;
}
