using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct C_Move : IComponentData
{
    public float Speed;
}

public struct C_ReachTarget : IComponentData
{
    public Entity Target;
    public float ReachDistance;
}
