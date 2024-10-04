using System.Collections.Generic;
using UnityEngine;

public class Patrol : IIdleBehaviour
{
    private const float MinDistanceToTarget = 0.05f;
    private float _speed;
    private Queue<Vector3> _targetPositions;

    private List<Transform> _targets;
    private Vector3 _currentTarget;

    private Transform _transform;
    private PlayerMovement _player;

    public Patrol(List<Transform> targets, Transform transform, float speed)
    {
        _targets = targets;
        _transform = transform;
        _speed = speed;
    } 

    private void ProcessMoveTo(Vector3 direction)
    {
        _transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void SwitchTarget()
    {
        _targetPositions.Enqueue(_currentTarget);

        _currentTarget = _targetPositions.Dequeue();
    }

    public void CreatQuene()
    {
        _targetPositions = new Queue<Vector3>();

        foreach (Transform target in _targets)
            _targetPositions.Enqueue(target.position);

        _currentTarget = _targetPositions.Dequeue();

        Debug.Log("Я создал очередь");
    }

    public void IdleBehaviour()
    {
        Vector3 direction = _currentTarget - _transform.position;

        Vector3 normalizedDirection = direction.normalized;

        if (direction.magnitude < MinDistanceToTarget)
            SwitchTarget();

        ProcessMoveTo(normalizedDirection);
        Debug.Log("Я патрулирую");
    }
}