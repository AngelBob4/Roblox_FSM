using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/CameraConfig")]
public class CameraConfig : ScriptableObject
{
    [field: SerializeField] public float Sensitivity { get; private set; } = 3f;
    [field: SerializeField] public float ZoomSpeed { get; private set; } = 0.25f;
    [field: SerializeField] public float MaxZoom { get; private set; } = 10f;
    [field: SerializeField] public float MinZoom { get; private set; } = 3f;
}
