using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

// Diviser en 2 (CoilHead et Bracken)

public class Monster : MonsterBehavior
{

    CharacterController controller;
    [SerializeField] private Transform target;
    [SerializeField] private float baseSpeed = 200f;
    float speed = 200f;
    [SerializeField] float angrySpeed = 400f;
    [SerializeField] private States _stateAI = States.CHASE;
    GameObject _zoneDetection;
    private Vector2 _velocity = Vector2.zero;
    StatesBehavior _state = StatesBehavior.AI;
    Transform _toChase;

    [SerializeField]
    float _baseSpeed = 200f;

    [SerializeField]
    float _angrySpeed = 400f;




    [SerializeField]
    MobType _mob = MobType.COILHEAD;


    bool isChasing => _toChase != null;
    

    float _timer = 0;

    // TIMES 
    float _timeStunned = 3;
    float _timeChasing = 5;

    //enum StatesBehavior
    //{
    //    NONE,
    //    AI,
    //    PLAYER
    //}

    //enum States
    //{
    //    NONE,
    //    WALKING, 
    //    FLASHED,
    //    CHASE,

    //}

    enum MobType
    {
        COILHEAD,
        BRACKEN
    }

    protected override void Init()
    {
        _speed = _baseSpeed;

        print("Start");
        if (GetComponent<CharacterController>() != null)
        {
            controller = GetComponent<CharacterController>();
        }
        _stateAI = States.CHASE;
    }


    //void Update()
    //{
        
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        if (_state == StatesBehavior.PLAYER)
    //        {
    //            _state = StatesBehavior.AI;
    //            print("Changed _state to AI");
    //        }
    //        else if (_state == StatesBehavior.AI) {
    //            _state = StatesBehavior.PLAYER;
    //            print("Changed _state to Player");
    //        }
            
    //    }

    //    switch (_state)
    //    {
            
    //        case StatesBehavior.NONE:
    //            break;
            
    //        case StatesBehavior.AI:
    //            Ai();
    //            break;
            
    //        case StatesBehavior.PLAYER:
    //            Player();
    //            break;

                
    //    }

    //    //controller.Move(_velocity * speed  * Time.deltaTime);
    //    if (controller != null && controller.enabled)
    //    {
    //        controller.Move(_velocity * Time.deltaTime);
    //    }

    //}

    protected override void Player()
    {

        _velocity = Vector2.zero;

        // INPUTS

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _velocity += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _velocity += new Vector2(1, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _velocity += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _velocity += new Vector2(0, -1);
        }

    }

    //void Ai()
    //{

    //    switch (_stateAI)
    //    {
    //        case States.NONE:
    //            break;
    //        case States.WALKING:
    //            Walking();
    //            break;
    //        case States.FLASHED:
    //            Flashed();
    //            break;
    //        case States.CHASE:
    //            Chase();
    //            break;
    //    }

    //}

    // Finds a point that he can access and travels to 

    // Visite un point des rooms ? 
    protected override void Walking()
    {

    }

    protected override void OnPlayerEnter(Collider2D other)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnPlayerExit(Collider2D other)
    {
        throw new System.NotImplementedException();
    }

    protected override void None()
    {
        throw new System.NotImplementedException();
    }

    // Stunned
    protected override void Flashed()
    {
        switch (_mob)
        {
            case MobType.COILHEAD:
                _aiPath.maxSpeed = 0;
                _timer += Time.deltaTime;
                if (_timer >= _timeStunned)
                {
                    if (_toChase != null) { _stateAI = States.CHASE; }
                    else { _stateAI = States.WALKING; }

                    _timer = 0;
                }
                break;
            case MobType.BRACKEN:
                _aiPath.maxSpeed = _speed;
                break;
        }


    }

    // Chases the player
    protected override void Chase()
    {
        _aiPath.maxSpeed = _speed;
        //_aiPath.maxSpeed = 2;
        //Debug.Log("Chasing"); 
        _aiPath.destination = target.position;
    }

    public override void FlashMonster(Vector3? playerPosition = null)
    {
        switch (_mob)
        {

            case MobType.COILHEAD:
                _stateAI = States.FLASHED;
                _timer = 0;
                break;

            case MobType.BRACKEN:
                if (_stateAI == States.FLASHED)
                {
                    _stateAI = States.CHASE;
                    _speed = _angrySpeed;
                }
                else
                {
                    _stateAI = States.FLASHED;
                    _speed = _baseSpeed;
                    
                    //_aiPath.destination = this.transform.position;
                    _aiPath.destination = new Vector2(-_toChase.position.x,  -_toChase.position.y);
                    //_aiPath.destination = _aiPath.
                }
                break;
        }


    }

    // When player enters the AI vision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //print("Player Detected");
            _stateAI = States.CHASE;
            _toChase = collision.gameObject.transform;
        }
    }


    // When player exits the AI vision
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _stateAI = States.WALKING;
            _toChase = null;
        }
    }
}
