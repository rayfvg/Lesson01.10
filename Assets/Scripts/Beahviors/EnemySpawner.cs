using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyBallPrefab;
    [SerializeField] private PlayerMovement _player;

    [SerializeField] private ParticleSystem _dieParticle;

    [SerializeField] private ReactionsToPlayer _reactionsToPlayer;
    [SerializeField] private BehaviorOfRest _restingsBehavior;

    private Enemy _enemyBall;

    private IBehaviour _iBehaviourIdle;
    private IBehaviour _iBehaviourTrigger;

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
        _runAwayFromPLayer = new RunAwayFromPLayer(_player, _enemyBall.transform, 4);
        _runAfterFromPlayer = new RunAfterFromPlayer(_player, _enemyBall.transform, 4);
        _enemyDie = new EnemyDie(_dieParticle, _enemyBall.transform, _enemyBall.gameObject);
    }
    private void Start()
    {
        if(_patrul != null)
        {
            _patrul.CreatQuene();
        }

        switch (_restingsBehavior)
        {
            case BehaviorOfRest.Stay:
                _iBehaviourIdle = _stay;
                break;

            case BehaviorOfRest.Walking:
                _iBehaviourIdle = _walker;
                break;

            case BehaviorOfRest.Patrol:
                _iBehaviourIdle = _patrul;
                break;
        }

        switch (_reactionsToPlayer)
        {
            case ReactionsToPlayer.RunAfterFromPlayer:
                _iBehaviourTrigger = _runAfterFromPlayer;
                break;

            case ReactionsToPlayer.RunAwayFromPlayer:
                _iBehaviourTrigger = _runAwayFromPLayer;
                break;

            case ReactionsToPlayer.Die:
                _iBehaviourTrigger = _enemyDie;
                break;
        }

        _enemyBall.Initialize(_iBehaviourIdle, _iBehaviourTrigger);
    }
}