using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private PlayerMovement _player;

    [SerializeField] private ParticleSystem _dieParticle;

    [SerializeField] private ReactionsToPlayer _reactionsToPlayer;
    [SerializeField] private BehaviorOfRest _restingsBehavior;

    private Enemy _enemy;

    private IBehaviour _iBehaviourIdle;
    private IBehaviour _iBehaviourTrigger;

    private List<Transform> _transformsListArray;

    public void StartSpawn()
    {
        _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

        _iBehaviourIdle = CreateIdleBehaviour(_enemy);
        _iBehaviourTrigger = CreateTriggerBehaviour(_enemy);

        _enemy.Initialize(_iBehaviourIdle, _iBehaviourTrigger);
    }

    private IBehaviour CreateIdleBehaviour(Enemy enemy)
    {
        switch (_restingsBehavior)
        {
            case BehaviorOfRest.Stay:
                return new Stay();

            case BehaviorOfRest.Walking:
                return new Walker(enemy.transform, 8);

            case BehaviorOfRest.Patrol:

                Transform[] _transformsList = _enemy.gameObject.GetComponentsInChildren<Transform>();

                _transformsListArray = new List<Transform>();
                foreach (Transform transform in _transformsList)
                {
                    _transformsListArray.Add(transform);
                }

                Patrol _patrol = new Patrol(_transformsListArray, enemy.transform, 8);
                _patrol.CreatQuene();
                return _patrol;

            default:
                return null;
        }
    }

    private IBehaviour CreateTriggerBehaviour(Enemy enemy)
    {
        switch (_reactionsToPlayer)
        {
            case ReactionsToPlayer.RunAfterFromPlayer:
                return new RunAfterFromPlayer(_player, enemy.transform, 4);

            case ReactionsToPlayer.RunAwayFromPlayer:
                return new RunAwayFromPLayer(_player, enemy.transform, 4);

            case ReactionsToPlayer.Die:
                return new dieBehaviour(_dieParticle, enemy.transform, enemy.gameObject);

            default:
                return null;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
            StartSpawn();
    }
}