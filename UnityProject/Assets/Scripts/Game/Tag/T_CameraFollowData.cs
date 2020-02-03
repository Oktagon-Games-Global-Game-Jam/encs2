using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
[GenerateAuthoringComponent]
public struct T_CameraFollowData : IComponentData
{
    public E_FollowType FollowType;
}

public enum E_FollowType
{
    Mouse,
    Mecha
}