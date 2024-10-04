using UnityEngine;

public class RunAwayFromPLayer : IReactionBehavior
{
    private PlayerMovement _player;
    private Transform _transform;
    private float _speed;

    public RunAwayFromPLayer(PlayerMovement player, Transform transform, float speed)
    {
        _player = player;
        _transform = transform;
        _speed = speed;
    }

    public void ReactionBehaviour()
    {
        Vector3 direction = _transform.position - _player.transform.position;

        Vector3 normalizedDirection = direction.normalized;

        _transform.Translate(normalizedDirection * _speed * Time.deltaTime, Space.World);

        Debug.Log("Я убегаю от игрока");
    }
}