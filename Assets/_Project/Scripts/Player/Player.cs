using UnityEngine;

public class Player : MonoBehaviour
{
    private GroundChecker _groundChecker;
    private PlayerConfig _playerConfig;
    private CameraRotateAround _cameraRotateAround;

    private PlayerStateMachine _playerStateMachine;
    private PlayerView _playerView;
    private InputSystem _inputSystem;
    private Rigidbody _rigidbody;

    public InputSystem InputSystem => _inputSystem;
    public PlayerView PlayerView => _playerView;
    public GroundChecker GroundChecker => _groundChecker;
    public Rigidbody Rigidbody => _rigidbody;
    public PlayerConfig PlayerConfig => _playerConfig;
    public CameraRotateAround CameraRotateAround => _cameraRotateAround;

    private void Awake()
    {
        _playerView = transform.GetComponentInChildren<PlayerView>();
        _rigidbody = transform.GetComponentInChildren<Rigidbody>();
        _playerView.Initialize();

        _inputSystem = new InputSystem();
        _playerStateMachine = new PlayerStateMachine(this);
    }

    private void Update() => _playerStateMachine.Update();

    private void OnEnable() => _inputSystem.Enable();

    private void OnDisable() => _inputSystem.Disable();

    public void Initialize(GroundChecker groundChecker, PlayerConfig playerConfig, CameraRotateAround cameraRotateAround)
    {
        _groundChecker = groundChecker;
        _playerConfig = playerConfig;
        _cameraRotateAround = cameraRotateAround;
    }
}
