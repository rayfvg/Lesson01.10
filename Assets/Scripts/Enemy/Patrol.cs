using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private const float MinDistanceToTarget = 0.05f;

    [SerializeField] private float _speed;
    [SerializeField] private List<Transform> _targets;

    private Queue<Vector3> _targetPositions;

    private Vector3 _currentTarget;

    private void Awake()
    {
        _targetPositions = new Queue<Vector3>();

        foreach (Transform target in _targets)
            _targetPositions.Enqueue(target.position);

        _currentTarget = _targetPositions.Dequeue();
    }

    private void Update()
    {
        Vector3 direction = _currentTarget - transform.position;

        Vector3 normalizedDirection = direction.normalized;

        if (direction.magnitude < MinDistanceToTarget)
            SwitchTarget();

        ProcessMoveTo(normalizedDirection);
    }

    private void ProcessMoveTo(Vector3 direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void SwitchTarget()
    {
        _targetPositions.Enqueue(_currentTarget);

        _currentTarget = _targetPositions.Dequeue();
    }
}