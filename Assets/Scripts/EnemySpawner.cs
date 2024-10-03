using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefabStay;
    [SerializeField] private Enemy _enemyPrefabPatrol;
    [SerializeField] private Enemy _enemyPrefabWalking;

    [SerializeField] private RestingsBehavior _restingsBehavior;

    [SerializeField] private ReactionsToPlayer _reactionsToPlayer;

    private Enemy _enemy;

    private void Start()
    {
        SpawnEnemy(_restingsBehavior);
    }
    public void SpawnEnemy(RestingsBehavior restingsBehavior)
    {
        switch (restingsBehavior)
        {
            case RestingsBehavior.Stay:
                _enemy = Instantiate(_enemyPrefabStay, transform.position, Quaternion.identity);
                _enemy.ReactionsToPlayer = _reactionsToPlayer;
                break;

            case RestingsBehavior.Patrol:
                _enemy = Instantiate(_enemyPrefabPatrol, transform.position, Quaternion.identity);
                _enemy.ReactionsToPlayer = _reactionsToPlayer;
                break;

            case RestingsBehavior.Walking:
                _enemy = Instantiate(_enemyPrefabWalking, transform.position, Quaternion.identity);
                _enemy.ReactionsToPlayer = _reactionsToPlayer;
                break;

            default:
                Debug.LogError("Нет такого состояния");
                break;
        }
    }
}