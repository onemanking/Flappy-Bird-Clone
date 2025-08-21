using UnityEngine;

[CreateAssetMenu(fileName = "BirdConfig", menuName = "Scriptable Objects/BirdConfig")]
public class BirdConfig : BasePlayerObjectConfig
{
    [field: SerializeField] internal float TiltAngle { get; private set; } = -30.0f;
    [field: SerializeField] internal float StartFallingVelocity { get; private set; } = -15.0f;
    [Range(0f, 1f)]
    [SerializeField] private float m_maxFallSpeedScale = 1f;

    internal float MaxFallSpeedScale => m_maxFallSpeedScale;
}
