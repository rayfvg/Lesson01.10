using UnityEngine;

public class Walk : MonoBehaviour
{
    private const float TimeToSwitchDirection = 1;

    [SerializeField] private float _speed;

    private Vector3 _currentTarget = new Vector3(0, 0, 1);

    private float _timer;


    private void Update()
    {
        transform.Translate(_currentTarget * _speed * Time.deltaTime);
        SwitchTarget();
    }

    private void SwitchTarget()
    {
        _timer += Time.deltaTime;
        if(_timer > TimeToSwitchDirection)
        {
            transform.eulerAngles = GetRandomAngle();
            _timer = 0;
        }
    }

    private Vector3 GetRandomAngle()
    {
        Vector3 angles = new Vector3(0, Random.Range(0, 360), 0);
        return angles;
    }
}