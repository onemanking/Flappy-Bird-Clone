using UnityEngine;

[CreateAssetMenu(fileName = "PipeSpawnConfig", menuName = "Scriptable Objects/PipeSpawnConfig")]
public class PipeSpawnConfig : ScriptableObject
{
    [Header("Pipe Spawn Settings")]
    [field: SerializeField] internal PipeContainer PipeContainerPrefab { get; private set; }
    [field: SerializeField] internal float SpawnInterval { get; private set; } = 2.0f;
}