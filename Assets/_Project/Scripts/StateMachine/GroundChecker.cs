using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;

    [SerializeField, Range(0.01f, 1)] private float _distanceToCheck;
    [SerializeField] private Transform _transformCube;

    public bool IsTouches { get; private set; }

    private void Update()
    {
        IsTouches = false;

        if (Physics.CheckSphere(_transformCube.position, _distanceToCheck, _ground))
            IsTouches = true;

        Debug.Log(IsTouches);
    }
}
