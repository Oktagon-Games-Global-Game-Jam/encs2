﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class S_Input : ComponentSystem
{
    public float2 lastFramePosition;
    protected override void OnCreate()
    {
        base.OnCreate();
        Entity tInputDataEntity = World.EntityManager.CreateEntity();
        World.EntityManager.AddComponent(tInputDataEntity, typeof(C_InputData));
    }

    protected override void OnUpdate()
    {
        bool tHolding = Input.GetMouseButton(0);

            float2 tCurrentFramePosition = new float2(Input.mousePosition.x, Input.mousePosition.y);
  
        Entities.ForEach((ref C_InputData inputData) =>
            {
                inputData.DeltaMove =  tHolding? tCurrentFramePosition - lastFramePosition : math.lerp( inputData.DeltaMove, float2.zero, 0.05f);
            });
        lastFramePosition = tCurrentFramePosition;
    }
}
