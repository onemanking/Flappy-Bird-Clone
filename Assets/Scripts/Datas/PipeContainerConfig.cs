using UnityEngine;

[CreateAssetMenu(fileName = "PipeContainerConfig", menuName = "Scriptable Objects/PipeContainerConfig")]
public class PipeContainerConfig : ScriptableObject
{
    [field: SerializeField] internal float BottomOffset { get; private set; } = 1.5f;
    [field: SerializeField] internal float MinTopPipeHeight { get; private set; } = 1f;
    [field: SerializeField] internal float MaxTopPipeHeight { get; private set; } = 5f;

    [field: SerializeField] internal float MinGap { get; private set; } = 2f;
    [field: SerializeField] internal float MaxGap { get; private set; } = 4f;
}
