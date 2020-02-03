using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct C_MechaPart : IComponentData
{
    public E_MechaPart MechaPart;
}
