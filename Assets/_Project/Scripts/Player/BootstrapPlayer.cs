using UnityEngine;

public class BootstrapPlayer : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private CameraRotateAround _cameraRotateAround;
    [SerializeField] private Player _player;

    private void Awake() => 
        _player.Initialize(_groundChecker, _playerConfig, _cameraRotateAround);
}
