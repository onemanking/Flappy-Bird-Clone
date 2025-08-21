using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Game Settings")]
    [field: SerializeField] internal float GroundLevel { get; private set; } = -4.0f;
}
