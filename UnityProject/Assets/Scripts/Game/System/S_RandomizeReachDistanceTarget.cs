using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(S_RemoveRecentlyCreatedTag))]
public class S_RandomizeReachDistanceTarget : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref C_ReachTarget reachTarget, ref C_Move move, ref T_Enemy enemy, ref T_RecentlyCreated recentlyCreated) =>
            {
                reachTarget.ReachDistance += Random.Range(-reachTarget.ReachDistance, reachTarget.ReachDistance);
                move.Speed += move.Speed * Random.Range(0f, 2f);
            });
    }
}