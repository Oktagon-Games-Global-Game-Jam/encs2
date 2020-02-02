using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


[GenerateAuthoringComponent]
public struct SC_ResourceMesh : ISharedComponentData, IEquatable<SC_ResourceMesh>
{
    public Mesh mesh;
    public Material material;

    public bool Equals(SC_ResourceMesh other)
    {
        return other.mesh == mesh;
    }

    public override int GetHashCode()
    {
        var hashCode = 1539557500;
        hashCode = hashCode * -1521134295 + EqualityComparer<Mesh>.Default.GetHashCode(mesh);
        hashCode = hashCode * -1521134295 + EqualityComparer<Material>.Default.GetHashCode(material);
        return hashCode;
    }
}

public struct C_Resource : IComponentData
{
    public int Value;
}

public struct C_Rotate : IComponentData
{
    public float Speed;
}
