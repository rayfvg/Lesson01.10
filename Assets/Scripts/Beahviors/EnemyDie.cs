using UnityEngine;

public class EnemyDie : IReactionBehavior
{
    private bool _allowedCreate = true;

    private ParticleSystem _dieParticle;
    private Transform _transform;
    private EnemyBall _thisEnemy;

    public EnemyDie(ParticleSystem dieParticle, Transform transform, EnemyBall enemy)
    {
        _dieParticle = dieParticle;
        _transform = transform;
        _thisEnemy = enemy;
    }

    public void ReactionBehaviour()
    {
        if (_allowedCreate)
        {
            _dieParticle.Play();
            _allowedCreate = false;
        }

        Object.Destroy(_thisEnemy);

        Debug.Log("Я взрываюсь при столкновении");

    }
}
