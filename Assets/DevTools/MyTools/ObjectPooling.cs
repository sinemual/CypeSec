using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject prefabObject = null;
    [SerializeField] private int poolDepth = 1;
    [SerializeField] private bool canGrow = false;

    private readonly List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolDepth; i++)
            CreatePoolObject();
    }

    public GameObject GetAvaliableObject()
    {
        for (int i = 0; i < pool.Count; i++)
            if (!pool[i].activeInHierarchy)
                return pool[i];

        if (canGrow)
            return CreatePoolObject();

        return null;
    }

    private GameObject CreatePoolObject()
    {
        GameObject _pooledObject = Instantiate(prefabObject);
        _pooledObject.SetActive(false);
        pool.Add(_pooledObject);
        return _pooledObject;
    }
}
