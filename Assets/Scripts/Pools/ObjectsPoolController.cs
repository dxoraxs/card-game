using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoolController<T> where T : MonoBehaviour, IPoolObject
{
    protected Queue<T> pool = new Queue<T>();
    protected List<T> usedObject = new List<T>();
    protected T prefabObject;
    protected Transform parent;

    public ObjectsPoolController(T prefab, Transform parent, int startCount)
    {
        prefabObject = prefab;
        this.parent = parent;

        for (int i = 0; i < startCount; i++)
        {
            var spawnNewObject = SpawnNewObject();
            pool.Enqueue(spawnNewObject);
            spawnNewObject.gameObject.SetActive(false);
        }
    }

    public virtual void ReturnAllObjectToPool()
    {
        foreach (var obj in usedObject)
        {
            pool.Enqueue(obj);
            obj.gameObject.SetActive(false);
            obj.OnReturnToPool();
        }

        usedObject.Clear();
    }

    public virtual void ReturnObject(T obj)
    {
        obj.transform.SetParent(parent);
        obj.OnReturnToPool();
        usedObject.Remove(obj);
        pool.Enqueue(obj);
    }

    public virtual T GetFreeObject()
    {
        if (!GetObjectFromList(out var currentObject))
        {
            currentObject = SpawnNewObject();
        }

        usedObject.Add(currentObject);
        return currentObject;
    }

    protected virtual T SpawnNewObject()
    {
        var spawnObject = Object.Instantiate(prefabObject, parent);
        spawnObject.name += "_" + (pool.Count + 1 + usedObject.Count);
        return spawnObject;
    }

    protected virtual bool GetObjectFromList(out T obj)
    {
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return true;
        }

        obj = null;
        return false;
    }
}