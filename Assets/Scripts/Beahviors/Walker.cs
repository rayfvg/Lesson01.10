using UnityEngine;

public class Walker : IBehaviour
{
    private const float TimeToSwitchDirection = 1;
    private float _speed = 5;
    private float _timer = 0;

    private Vector3 _currentTarget = new Vector3(0, 0, 1);

    private Transform _transform;
    private Enemy _enemyBall;

    public Walker(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    private void SwitchTarget()
    {
        _timer += Time.deltaTime;
        if (_timer > TimeToSwitchDirection)
        {
            _transform.eulerAngles = GetRandomAngle();
            _timer = 0;
        }
    }

    private Vector3 GetRandomAngle()
    {
        Vector3 angles = new Vector3(0, Random.Range(0, 360), 0);
        return angles;
    }

    public void Update()
    {
        _transform.Translate(_currentTarget * _speed * Time.deltaTime);
        SwitchTarget();
        Debug.Log("� ���");
    }
}