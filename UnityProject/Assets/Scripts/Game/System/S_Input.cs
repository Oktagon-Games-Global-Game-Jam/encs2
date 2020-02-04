using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_Input : ComponentSystem
{
    public float2 lastFramePosition;
    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnUpdate()
    {
        bool tHolding = Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject();

        float2 tCurrentFramePosition = new float2(Input.mousePosition.x, Input.mousePosition.y);
  
        Entities.ForEach((ref C_InputData inputData) =>
            {
                inputData.DeltaMove =  tHolding? lastFramePosition - tCurrentFramePosition : math.lerp( inputData.DeltaMove, float2.zero, 0.05f);
            });
        lastFramePosition = tCurrentFramePosition;
    }
}
