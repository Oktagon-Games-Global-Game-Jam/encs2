using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECSComponentMono <T> where T : Object
{
    private Dictionary<int, T> m_Objects;

    public ECSComponentMono(T[] pComponents)
    {
        m_Objects = new Dictionary<int, T>();

        for (int i = 0; i < pComponents.Length; i++)
        {
            AddObject(pComponents[i].GetInstanceID(), pComponents[i]);            
        }
    }

    public ECSComponentMono()
    {
        m_Objects = new Dictionary<int, T>();
    }
    public void AddObject(int pId, T pMonoObject)
    {
        if (m_Objects.ContainsKey(pId))
            m_Objects[pId] = pMonoObject;
        else
            m_Objects.Add(pId, pMonoObject);
    }

    public void RemoveObject(int pId)
    {
        if (m_Objects.ContainsKey(pId))
        {
            m_Objects.Remove(pId);
        }
    }

    public T GetObject(int pId)
    {
        return m_Objects[pId];
    }
}
