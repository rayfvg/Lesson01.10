using System.Collections.Generic;
using UnityEngine;

public class ExampleSpawner : MonoBehaviour
{
    [SerializeField] private EnemyBall _enemyBallPrefab;
    [SerializeField] private PlayerMovement _player;

    [SerializeField] private ParticleSystem _dieParticle;

    [SerializeField] private ReactionsToPlayer _reactionsToPlayer;
    [SerializeField] private RestingsBehavior _restingsBehavior;

    private EnemyBall _enemyBall;

    private IIdleBehaviour _idleBehaviour;
    private IReactionBehavior _reactionBehavior;

    private Transform[] _transformsList;

    private Walker _walker;
    private Patrul _patrul;
    private Stay _stay;
    private RunAwayFromPLayer _runAwayFromPLayer;
    private RunForPlayer _runForPlayer;
    private EnemyDie _enemyDie;
    private void Awake()
    {
        _transformsList = _enemyBallPrefab.GetComponentsInChildren<Transform>();
        List<Transform> _transformsListArray = new List<Transform>();
        foreach (Transform transform in _transformsList)
        {
            _transformsListArray.Add(transform);
        }

        
        _walker = new Walker(_enemyBallPrefab.transform, 8);
        _patrul = new Patrul(_transformsListArray, _enemyBallPrefab.transform, 8);
        _stay = new Stay();
        _runAwayFromPLayer = new RunAwayFromPLayer(_player, _enemyBallPrefab.transform, 8);
        _runForPlayer = new RunForPlayer(_player, _enemyBallPrefab.transform, 8);
        _enemyDie = new EnemyDie(_dieParticle, _enemyBallPrefab.transform, _enemyBall);

        switch (_restingsBehavior)
        {
            case RestingsBehavior.Stay:
                _idleBehaviour = _stay;
                break;

            case RestingsBehavior.Walking:
                _idleBehaviour = _walker;
                break;

            case RestingsBehavior.Patrol:
                _idleBehaviour = _patrul;
                break;
        }

        switch (_reactionsToPlayer)
        {
            case ReactionsToPlayer.RunAfterPlayer:
                _reactionBehavior = _runForPlayer;
                break;

            case ReactionsToPlayer.EscapeFromPlayer:
                _reactionBehavior = _runAwayFromPLayer;
                break;

            case ReactionsToPlayer.Die:
                _reactionBehavior = _enemyDie;
                break;
        }

        _enemyBall = Instantiate(_enemyBallPrefab, transform.position, Quaternion.identity);
        _enemyBall.Initialize(_idleBehaviour, _reactionBehavior);
    }

}
