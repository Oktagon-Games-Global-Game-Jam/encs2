using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] HUDView m_HUDView;
    private Dictionary<E_MechaPart, Entity> m_SpawnEntities;

    private void Start()
    {
        m_SpawnEntities = new Dictionary<E_MechaPart, Entity>();
        m_HUDView.Setup();
        m_HUDView.SetOnClick(0, () => StartSpawn(E_MechaPart.Legs), () => EndSpawn(E_MechaPart.Legs));
        m_HUDView.SetOnClick(1, () => StartSpawn(E_MechaPart.Body), () => EndSpawn(E_MechaPart.Body));
        m_HUDView.SetOnClick(2, () => StartSpawn(E_MechaPart.Head), () => EndSpawn(E_MechaPart.Head));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartSpawn(E_MechaPart.Legs);
        if (Input.GetKeyUp(KeyCode.Alpha1))
            EndSpawn(E_MechaPart.Legs);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            StartSpawn(E_MechaPart.Body);
        if (Input.GetKeyUp(KeyCode.Alpha2))
            EndSpawn(E_MechaPart.Body);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            StartSpawn(E_MechaPart.Head);
        if (Input.GetKeyUp(KeyCode.Alpha3))
            EndSpawn(E_MechaPart.Head);
    }

    public void StartSpawn(E_MechaPart ePart)
    {
        if (m_SpawnEntities.ContainsKey(ePart))
            return;
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity tEntity = manager.CreateEntity();
        manager.AddComponentData(tEntity, new C_UISpawnUnitRequest() { MechaPart = ePart });


        // store it
        m_SpawnEntities.Add(ePart, tEntity);
    }
    public void EndSpawn(E_MechaPart ePart)
    {
        if (!m_SpawnEntities.ContainsKey(ePart))
            return;
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        manager.DestroyEntity(m_SpawnEntities[ePart]);
        m_SpawnEntities.Remove(ePart);
    }
}
