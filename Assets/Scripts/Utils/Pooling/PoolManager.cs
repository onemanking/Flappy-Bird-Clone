using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private PoolingConfig m_poolingConfig;

    private readonly Dictionary<int, object> pools = new Dictionary<int, object>();

    public IObjectPool<T> GetPool<T>(T prefab) where T : MonoBehaviour, IPool
    {
        int instanceID = prefab.GetInstanceID();

        if (pools.TryGetValue(instanceID, out object existingPool))
        {
            return (IObjectPool<T>)existingPool;
        }

        IObjectPool<T> newPool = new ObjectPool<T>(
            createFunc: () => CreatePooledItem(prefab),
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: m_poolingConfig.CollectionCheck,
            defaultCapacity: m_poolingConfig.DefaultCapacity,
            maxSize: m_poolingConfig.MaxPoolSize
        );

        pools[instanceID] = newPool;
        return newPool;
    }

    private T CreatePooledItem<T>(T prefab) where T : MonoBehaviour, IPool
    {
        T obj = Instantiate(prefab);
        obj.gameObject.name = $"{prefab.name}_pooled";
        return obj;
    }

    private void OnTakeFromPool<T>(T obj) where T : MonoBehaviour, IPool
    {
        obj.gameObject.SetActive(true);
        obj.OnSpawn();
    }

    private void OnReturnedToPool<T>(T obj) where T : MonoBehaviour, IPool
    {
        obj.gameObject.SetActive(false);
        obj.OnDespawn();
    }

    private void OnDestroyPoolObject<T>(T obj) where T : MonoBehaviour
    {
        Destroy(obj.gameObject);
    }
}
