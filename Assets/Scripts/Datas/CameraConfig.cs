using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Scriptable Objects/CameraConfig")]
public class CameraConfig : ScriptableObject
{
    [field: SerializeField] internal Vector3 Offset { get; private set; } = new Vector3(1, 0, 0);
    [field: SerializeField] internal float SmoothSpeed { get; private set; } = 0.125f;
}
