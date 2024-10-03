using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dieParticle;

    [SerializeField] private float _speed;

    private PlayerMovement _player;
    private bool _allowedCreate = true;

    private Patrol _patrol;
    private Walk _walk;

    public ReactionsToPlayer ReactionsToPlayer;

    private void OnTriggerEnter(Collider other)
    {
        _patrol = GetComponent<Patrol>();
        _walk = GetComponent<Walk>();
        if (_patrol != null)
            _patrol.enabled = false;
        if (_walk != null)
            _walk.enabled = false;

        _player = other.GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_player != null)
        {
            switch (ReactionsToPlayer)
            {
                case ReactionsToPlayer.RunAfterPlayer:
                    RunAfterPlayer();
                    break;

                case ReactionsToPlayer.EscapeFromPlayer:
                    EscapeFromPlayer();
                    break;

                case ReactionsToPlayer.Die:
                    Die();
                    break;

                default:
                    Debug.LogError("Нет такой реакции");
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_patrol != null)
            _patrol.enabled = true;
        if (_walk != null)
            _walk.enabled = true;
    }

    private void Die()
    {
        if (_allowedCreate)
        {
            Instantiate(_dieParticle, transform.position, Quaternion.Euler(-90, 0, 0));
            _allowedCreate = false;
        }

        Destroy(gameObject);
    }

    private void RunAfterPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;

        ProcessMoveTo(direction);
    }

    private void EscapeFromPlayer()
    {
        Vector3 direction = transform.position - _player.transform.position;

        ProcessMoveTo(direction);
    }

    private void ProcessMoveTo(Vector3 direction)
    {
        Vector3 normalizedDirection = direction.normalized;
        transform.Translate(normalizedDirection * _speed * Time.deltaTime, Space.World);
    }
}