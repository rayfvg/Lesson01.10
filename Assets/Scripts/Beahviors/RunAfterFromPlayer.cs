using UnityEngine;

public class RunAfterFromPlayer : IReactionBehavior
{
    private PlayerMovement _player;
    private Transform _transform;
    private float _speed;

    public RunAfterFromPlayer(PlayerMovement player, Transform transform, float speed)
    {
        _player = player;
        _transform = transform;
        _speed = speed;
    }

    public void ReactionBehaviour()
    {
        Vector3 direction = _player.transform.position - _transform.position;

        Vector3 normalizedDirection = direction.normalized;

        _transform.Translate(normalizedDirection * _speed * Time.deltaTime, Space.World);

        Debug.Log("Я бегу за игроком");
    }
}