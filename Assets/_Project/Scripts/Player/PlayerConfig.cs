using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; } = 3f;
    [field: SerializeField] public float JumpForce { get; private set; } = 4f;
    [field: SerializeField] public float SpeedRotation { get; private set; } = 8f;
}
