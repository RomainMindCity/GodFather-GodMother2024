using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBehavior : MonoBehaviour
{

    protected float _speed;

    [Header("Values GD")]
    [SerializeField] protected bool _activated = true;
    [SerializeField] protected float _radiusDetection;

    protected bool _canBeControlled = false;

    protected List<string> tagsWalls = new List<string> { "Wall", "Door", "Environement" };

    protected States _stateAI = States.NONE;
    protected StatesBehavior _state = StatesBehavior.AI;

    [SerializeField] protected bool _needToSee;

    protected Transform _toChase;

    protected AIPath _aiPath;

    protected GameObject _zoneDetection;

    protected Vector2 _velocity = Vector2.zero;

    protected CircleCollider2D _collider2D;

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
        _collider2D = GetComponent<CircleCollider2D>();
        _collider2D.radius = _radiusDetection;

        Init();
    }

    protected abstract void Init();

    void Update() 
    {

        if (_canBeControlled)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (_state == StatesBehavior.PLAYER)
                {
                    _state = StatesBehavior.AI;
                    _aiPath.canMove = true;
                }
                else
                {
                    _state = StatesBehavior.PLAYER;
                    _aiPath.canMove = false;
                    transform.rotation = Quaternion.identity;
                }
            }
        }

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

    /// <summary>
    /// Function you can call to unflash the monster (when the monster is out of the flashlight)
    /// </summary>
    public abstract void UnflashMonster();



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Enter");
        OnPlayerEnter(collision);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Exit");
        OnPlayerExit(other);
    }

    

    protected abstract void OnPlayerEnter(Collider2D other);
    protected abstract void OnPlayerExit(Collider2D other);

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

    /// <summary>
    /// Check if there is a wall between the monster and the player
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    protected bool checkWalls(Collider2D other)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, Vector2.Distance(transform.position, other.transform.position), LayerMask.GetMask("Wall"));

        if (hit.collider != null && tagsWalls.Contains(hit.collider.tag))
        {
            return true;
        }

        return false;
    }

    protected bool checkWalls(Transform other)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, Vector2.Distance(transform.position, other.transform.position), LayerMask.GetMask("Wall"));

        if (hit.collider != null && tagsWalls.Contains(hit.collider.tag))
        {
            return true;
        }

        return false;
    }

}
