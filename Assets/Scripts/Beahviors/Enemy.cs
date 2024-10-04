using UnityEngine;

public class Enemy : MonoBehaviour
{
    private IBehaviour _behaviourIdle;
    private IBehaviour _behaviourTrigger;

    private IBehaviour _currentBehaviour;

    private void Start()
    {
        _currentBehaviour = _behaviourIdle;
    }

    public void Update()
    {
        _currentBehaviour.Update();
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            _currentBehaviour = _behaviourTrigger;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _currentBehaviour = _behaviourIdle;
    }

    public void Initialize(IBehaviour behaviourIdle, IBehaviour behaviourTrigger)
    {
        _behaviourIdle = behaviourIdle;
        _behaviourTrigger = behaviourTrigger;
    }
}