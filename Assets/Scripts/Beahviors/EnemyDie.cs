using UnityEngine;

public class EnemyDie : IBehaviour
{
    private bool _allowedCreate = true;

    private ParticleSystem _dieParticle;
    private Transform _transform;
    private GameObject _thisEnemy;

    public EnemyDie(ParticleSystem dieParticle, Transform transform, GameObject enemy)
    {
        _dieParticle = dieParticle;
        _transform = transform;
        _thisEnemy = enemy;
    }

    public void Update()
    {
        if (_allowedCreate)
        {
            Object.Instantiate(_dieParticle, _transform.position, Quaternion.Euler(-90, 0, 0));
            _allowedCreate = false;
        }

        Object.Destroy(_thisEnemy);

        Debug.Log("Я взрываюсь при столкновении");
    }
}