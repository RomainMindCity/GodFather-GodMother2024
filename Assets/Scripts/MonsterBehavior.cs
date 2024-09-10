using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBehavior : MonoBehaviour
{

    [SerializeField] protected float _speed;

    protected bool canBeControlled = false;


    protected States _stateAI = States.CHASE;
    protected StatesBehavior _state = StatesBehavior.AI;

    protected Transform _toChase;

    protected AIPath _aiPath;

    protected GameObject _zoneDetection;

    protected Vector2 _velocity = Vector2.zero;

    protected enum StatesBehavior
    {
        NONE,
        AI,
        PLAYER
    }

    protected enum States
    {
        NONE,
        WALKING,
        FLASHED,
        CHASE,

    }

    void Start() 
    {
        _aiPath = GetComponent<AIPath>();
        _zoneDetection = transform.Find("Zone").gameObject;
    }

    protected abstract void Init();

    void Update() 
    {
        switch (_state)
        {
            case StatesBehavior.NONE:
                break;

            case StatesBehavior.AI:
                Ai();
                break;

            case StatesBehavior.PLAYER:
                Player();
                break;
        }
    }

    void Ai()
    {
        switch (_stateAI)
        {
            case States.NONE:
                None();
                break;
            case States.WALKING:
                Walking();
                break;
            case States.FLASHED:
                Flashed();
                break;
            case States.CHASE:
                Chase();
                break;
        }
    }

   

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            _toChase = other.transform;
            OnPlayerEnter(other);
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _toChase = null;
            OnPlayerExit(other);
        }
    }

    protected abstract void OnPlayerEnter(Collider other);
    protected abstract void OnPlayerExit(Collider other);
    protected abstract void Flashed();
    protected abstract void Chase();
    protected abstract void Walking();
    protected abstract void None();

    protected abstract void Player();

}
