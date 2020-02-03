using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Rendering;

public class JS_Rotate : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float time = Time.DeltaTime;
        return
        Entities
            .ForEach((ref Rotation pRot, in C_Rotate pRotate) =>
            {
                pRot.Value = math.mul(pRot.Value, quaternion.RotateZ(math.radians(pRotate.Speed * time)));
            })
            .Schedule(inputDeps);
    }
}