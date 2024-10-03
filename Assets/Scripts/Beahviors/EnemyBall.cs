using UnityEngine;

public class EnemyBall : MonoBehaviour, Iidle, IReaction
{
    private IIdleBehaviour _idleBehaviour;
    private IReactionBehavior _reactionBehavior;

    private bool _isReactions = true;

    public void Update()
    {
        if(_isReactions)
        Idle();
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Reactions();
            _isReactions = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isReactions = true;
    }

    public void Initialize(IIdleBehaviour idleBehaviour, IReactionBehavior reactionBehavior)
    {
        _idleBehaviour = idleBehaviour;
        _reactionBehavior = reactionBehavior;
    }
    public void Idle() => _idleBehaviour.IdleBehaviour();

    public void Reactions() => _reactionBehavior.ReactionBehaviour();
}