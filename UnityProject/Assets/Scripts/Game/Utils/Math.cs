using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;

class Math
{
    public static float DistanceXZ(float3 v1, float3 v2)
    {
        float xDiff = v1.x - v2.x;
        float zDiff = v1.z - v2.z;
        return math.sqrt((xDiff * xDiff) + (zDiff * zDiff));
    }
}