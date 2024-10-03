using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private float _deadZone = 0.1f;
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw(HorizontalAxisName);
        float inputY = Input.GetAxisRaw(VerticalAxisName);

        Vector3 inputXY = new Vector3(inputX, 0, inputY);

        if (inputXY.magnitude <= _deadZone)
        {
            return;
        }

        _characterController.Move(inputXY.normalized * Time.deltaTime * _speed);

        Quaternion lookRotation = Quaternion.LookRotation(inputXY.normalized);
        float step = _rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
    }
}