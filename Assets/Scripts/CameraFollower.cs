using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        _camera.transform.position = _target.position + _offset;
    }
}