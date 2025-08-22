using UnityEngine;

[CreateAssetMenu(fileName = "PipeData", menuName = "Scriptable Objects/PipeData")]
public class PipeData : ScriptableObject
{
    [field: SerializeField] public Pipe PipePrefab { get; private set; }
}
