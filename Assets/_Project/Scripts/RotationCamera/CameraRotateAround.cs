using UnityEngine;

public class CameraRotateAround : MonoBehaviour
{
    private const string MouseScrollWheel = "Mouse ScrollWheel";

    [SerializeField] private CameraConfig cameraConfig;
    [SerializeField] private Transform _target;
    [SerializeField] private float _verticalAngleLimit = 80f;
    [SerializeField] private Vector3 _offset;


    private int _rightButton = 1;
    private float _horizontalAngle;
    private float _verticalAngle;
    private Vector3 _cameraDirection;

    public Vector3 CameraDirection => _cameraDirection;

    private void Start()
    {
        _verticalAngleLimit = Mathf.Abs(_verticalAngleLimit);
        if (_verticalAngleLimit > 90) _verticalAngleLimit = 90;

        _offset = new Vector3(_offset.x, _offset.y, -Mathf.Abs(cameraConfig.MaxZoom) / 2);
        UpdateCameraPosition();
    }

    private void Update()
    {
        HandleZoom();
        HandleRotationInput();
    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
        UpdateCameraDirection(); 
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis(MouseScrollWheel);

        if (scroll != 0)
        {
            _offset.z += scroll * cameraConfig.ZoomSpeed;
            _offset.z = Mathf.Clamp(_offset.z, -Mathf.Abs(cameraConfig.MaxZoom), -Mathf.Abs(cameraConfig.MinZoom));
        }
    }

    private void HandleRotationInput()
    {
        if (Input.GetMouseButton(_rightButton))
        {
            _horizontalAngle += Input.GetAxis("Mouse X") * cameraConfig.Sensitivity;
            _verticalAngle -= Input.GetAxis("Mouse Y") * cameraConfig.Sensitivity;
            _verticalAngle = Mathf.Clamp(_verticalAngle, -_verticalAngleLimit, _verticalAngleLimit);
        }
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0);
        transform.position = _target.position + rotation * _offset;
        transform.LookAt(_target.position);
    }

    private void UpdateCameraDirection() =>
        _cameraDirection = transform.forward;
}
