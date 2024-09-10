using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBehavior : MonoBehaviour
{

    protected float _speed;

    protected bool canBeControlled = false;


    protected States _stateAI = States.NONE;
    protected StatesBehavior _state = StatesBehavior.AI;

    [SerializeField]
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

        Init();
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


    /// <summary>
    /// Function called when the monster is in the flashlight
    /// </summary>
    public abstract void FlashMonster(Vector3? playerPosition = null);

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

    /// <summary>
    /// Function that is called when the monster is Flashed
    /// </summary>
    protected abstract void Flashed();

    /// <summary>
    /// Function that is called when the monster is in the chase state
    /// </summary>
    protected abstract void Chase();

    /// <summary>
    /// Function that is called for the walking state of the AI
    /// </summary>
    protected abstract void Walking();

    /// <summary>
    /// Function that is called for the none state of the AI
    /// </summary>
    protected abstract void None();

    /// <summary>
    /// Function that is called when the monster is controlled (state = Player)
    /// </summary>
    protected abstract void Player();

}
