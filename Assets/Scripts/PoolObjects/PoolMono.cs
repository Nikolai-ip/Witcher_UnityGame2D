using System.Collections.Generic;
using UnityEngine;


public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; } 
    public Transform Container { get; set; }
    private List<T> _pool;
    public PoolMono(T prefab, int poolCapacity)
    {
        Prefab = prefab;
        Container = null;
        CreatePool(poolCapacity);
    }
    public PoolMono(T prefab, int poolCapacity, Transform container)
    {
        Prefab = prefab;
        Container = container;
        CreatePool(poolCapacity);
    }
    private void CreatePool(int capacity)
    {
        _pool = new List<T>();
        for (int i = 0; i < capacity; i++)
        {
            _pool.Add(CreateObject());
        }
    }
    private T CreateObject(bool isActiveByDefault = false)
    {
        var instance = UnityEngine.Object.Instantiate(Prefab, Container);
        instance.gameObject.SetActive(isActiveByDefault);
        return instance;
    }
    public bool HasFreeElement(out T element)
    {
        foreach (var obj in _pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                element = obj;
                obj.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }
    public T GetFreeElement()
    {
        if (HasFreeElement(out T element))
        {
            return element;
        }
        if (AutoExpand)
            return CreateObject(true);
        throw new System.Exception($"Pool<{typeof(T)}is overflow");
    }
    public T[] GetFreeElements(int count)
    {
        T[] elements = new T[count];    
        for (int i = 0; i < elements.Length; i++)
        {
             elements[i] = GetFreeElement();
        }
        return elements;
    }
}
