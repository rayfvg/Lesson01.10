using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBall _enemyBallPrefab;
    [SerializeField] private PlayerMovement _player;

    [SerializeField] private ParticleSystem _dieParticle;

    [SerializeField] private ReactionsToPlayer _reactionsToPlayer;
    [SerializeField] private BehaviorOfRest _restingsBehavior;

    private EnemyBall _enemyBall;

    private IIdleBehaviour _idleBehaviour;
    private IReactionBehavior _reactionBehavior;

    private Transform[] _transformsList;

    private Walker _walker;
    private Patrol _patrul;
    private Stay _stay;
    private RunAwayFromPLayer _runAwayFromPLayer;
    private RunAfterFromPlayer _runAfterFromPlayer;
    private EnemyDie _enemyDie;

    private void Awake()
    {
        _enemyBall = Instantiate(_enemyBallPrefab, transform.position, Quaternion.identity);

        _transformsList = _enemyBall.GetComponentsInChildren<Transform>();
        List<Transform> _transformsListArray = new List<Transform>();
        foreach (Transform transform in _transformsList)
        {
            _transformsListArray.Add(transform);
        }

        _walker = new Walker(_enemyBall.transform, 8);
        _patrul = new Patrol(_transformsListArray, _enemyBall.transform, 8);
        _stay = new Stay();
        _runAwayFromPLayer = new RunAwayFromPLayer(_player, _enemyBall.transform, 8);
        _runAfterFromPlayer = new RunAfterFromPlayer(_player, _enemyBall.transform, 8);
        _enemyDie = new EnemyDie(_dieParticle, _enemyBall.transform, _enemyBall.gameObject);

        switch (_restingsBehavior)
        {
            case BehaviorOfRest.Stay:
                _idleBehaviour = _stay;
                break;

            case BehaviorOfRest.Walking:
                _idleBehaviour = _walker;
                break;

            case BehaviorOfRest.Patrol:
                _idleBehaviour = _patrul;
                break;
        }

        switch (_reactionsToPlayer)
        {
            case ReactionsToPlayer.RunAfterFromPlayer:
                _reactionBehavior = _runAfterFromPlayer;
                break;

            case ReactionsToPlayer.RunAwayFromPlayer:
                _reactionBehavior = _runAwayFromPLayer;
                break;

            case ReactionsToPlayer.Die:
                _reactionBehavior = _enemyDie;
                break;
        }

        _enemyBall.Initialize(_idleBehaviour, _reactionBehavior);
    }
    private void Start()
    {
        if(_patrul != null)
        {
            _patrul.CreatQuene();
        }
    }
}