using UnityEngine;

public class RunForPlayer : IReactionBehavior
{
    private PlayerMovement _player;
    private Transform _transform;
    private float _speed;

    public RunForPlayer(PlayerMovement player, Transform transform, float speed)
    {
        _player = player;
        _transform = transform;
        _speed = speed;
    }

    public void Update()
    {
        ReactionBehaviour();
    }

    public void ReactionBehaviour()
    {
        Vector3 direction = _transform.position - _player.transform.position;

        Vector3 normalizedDirection = direction.normalized;

        _transform.Translate(normalizedDirection * _speed * Time.deltaTime, Space.World);

        Debug.Log("Я бегу за игроком");
    }
}
