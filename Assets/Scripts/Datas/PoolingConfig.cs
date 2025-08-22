using UnityEngine;

[CreateAssetMenu(fileName = "PoolingConfig", menuName = "Scriptable Objects/PoolingConfig")]
public class PoolingConfig : ScriptableObject
{
    [field: SerializeField] internal int DefaultCapacity { get; private set; } = 10;
    [field: SerializeField] internal int MaxPoolSize { get; private set; } = 100;
    [field: SerializeField] internal bool CollectionCheck { get; private set; } = true;
}
