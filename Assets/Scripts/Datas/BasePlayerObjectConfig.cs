using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BasePlayerObjectConfig", menuName = "Scriptable Objects/BasePlayerObjectConfig")]
public class BasePlayerObjectConfig : ScriptableObject
{
    [Header("Base Object Settings")]
    [field: SerializeField] internal float Speed { get; private set; } = 2.0f;
    [field: SerializeField] internal float JumpForce { get; private set; } = 5.0f;
    [field: SerializeField] internal float GravityScale { get; private set; } = 1.0f;

#if UNITY_EDITOR
    internal event Action OnEditorValidate;

    private void OnValidate()
    {
        OnEditorValidate?.Invoke();
    }
#endif
}
