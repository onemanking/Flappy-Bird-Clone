using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager
{
    [SerializeField] private int maxPoolSize = 100;
    [SerializeField] private bool collectionCheck = true;

    // private readonly Queue<IPool> poolQueue = new();

    // internal void RegisterPool(IPool pool)
    // {
    //     if (pool == null)
    //     {
    //         Debug.LogError("Attempted to register a null pool.");
    //         return;
    //     }

    //     poolQueue.Enqueue(pool);
    // }

    // internal IPool GetPool()
    // {
    //     if (poolQueue.Count > 0)
    //     {
    //         return poolQueue.Dequeue();
    //     }

    //     Debug.LogWarning("No available pools.");
    //     return null;
    // }

    // internal void ReturnPool(IPool pool)
    // {
    //     if (pool == null)
    //     {
    //         Debug.LogError("Attempted to return a null pool.");
    //         return;
    //     }

    //     poolQueue.Enqueue(pool);
    // }
    // internal IObjectPool<IPool> Pool { get; private set; }

    // protected override void Awake()
    // {
    //     base.Awake();
    //     Initialize();
    // }

    // private void Initialize()
    // {
    //     Pool = new ObjectPool<IPool>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionCheck, 10, maxPoolSize);
    // }

    // private void OnDestroyPoolObject(IPool objectPool)
    // {
    //     Destroy(objectPool as MonoBehaviour.gameObject);
    // }

    // private void OnReturnedToPool(IPool objectPool)
    // {
    //     throw new NotImplementedException();
    // }

    // private void OnTakeFromPool(IPool objectPool)
    // {
    //     throw new NotImplementedException();
    // }

    // private IPool CreatePooledItem()
    // {

    // }
}
