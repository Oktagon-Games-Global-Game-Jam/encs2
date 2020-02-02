using Unity.Entities;
using Unity.Mathematics;



public struct SpawnUnitRequest : IComponentData
{
    public E_MechaPart MechaPart;
}