using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct C_Life : IComponentData
{
    public int fullLife;
    public int actualLife;
}
