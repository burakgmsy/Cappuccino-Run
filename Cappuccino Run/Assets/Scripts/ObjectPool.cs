using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{

    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;

        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools = null;

    private void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>();
            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].objectPrefab);
                obj.SetActive(false);
                pools[j].pooledObjects.Enqueue(obj);
                obj.transform.parent = gameObject.transform;
            }
        }
    }

    public GameObject GetPooledObject(int objectType, GameObject gameObject)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }
        GameObject obj = pools[objectType].pooledObjects.Dequeue();

        obj.SetActive(true);

        pools[objectType].pooledObjects.Enqueue(obj);

        obj.transform.position = gameObject.transform.position;

        return obj;
    }
    public void AllObjDeActived()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject obj = pools[j].pooledObjects.Dequeue();
                obj.SetActive(false);
                pools[j].pooledObjects.Enqueue(obj);
            }
        }
    }
}
