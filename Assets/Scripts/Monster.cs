using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

// Diviser en 2 (CoilHead et Bracken)

public class Monster : MonoBehaviour
{

    

    private AIPath _aiPath;

    CharacterController controller;

    [SerializeField] private Transform target;

    [SerializeField]
    float baseSpeed = 200f;

    float speed = 200f;

    [SerializeField]
    float angrySpeed = 400f;

    States _stateAI = States.CHASE;

    GameObject _zoneDetection;

    private Vector2 _velocity = Vector2.zero;

    StatesBehavior _state = StatesBehavior.AI;

    Transform _toChase;

    bool isChasing => _toChase != null;
    

    float _timer = 0;

    // TIMES 
    float _timeStunned = 3;
    float _timeChasing = 5;

    enum StatesBehavior
    {
        NONE,
        AI,
        PLAYER
    }

    enum States
    {
        NONE,
        WALKING, 
        FLASHED,
        CHASE,

    }

    enum MobType
    {
        COILHEAD,
        BRACKEN
    }

    void Start()
    {
        _aiPath = GetComponent<AIPath>();
        _zoneDetection = transform.Find("Zone").gameObject;

        //_state = StatesBehavior.NONE;

        print("Start");
        //controller = gameObject.AddComponent<CharacterController>();
        if (GetComponent<CharacterController>() != null)
        {
            controller = GetComponent<CharacterController>();
        }

    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_state == StatesBehavior.PLAYER)
            {
                _state = StatesBehavior.AI;
                print("Changed _state to AI");
            }
            else if (_state == StatesBehavior.AI) {
                _state = StatesBehavior.PLAYER;
                print("Changed _state to Player");
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

        //controller.Move(_velocity * speed  * Time.deltaTime);
        if (controller != null && controller.enabled)
        {
            controller.Move(_velocity * Time.deltaTime);
        }

    }

    void Player()
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

    void Ai()
    {

        switch (_stateAI)
        {
            case States.NONE:
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

    // Finds a point that he can access and travels to 

    // Visite un point des rooms ? 
    void Walking()
    {

    }
    
    // Stunned
    void Flashed()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeStunned)
        {
            if (_toChase != null) { _stateAI = States.CHASE; }
            else                  { _stateAI = States.WALKING; }

            _timer = 0;
        }
    }

    // Chases the player
    void Chase()
    {
        _aiPath.maxSpeed = 2;
        //Debug.Log("Chasing"); 
        _aiPath.destination = target.position;
    }

    public void FlashMonster()
    {
        if (_stateAI == States.FLASHED)
        {
            _stateAI = States.CHASE;
            speed = angrySpeed;
        } 
        else
        {
            _stateAI = States.FLASHED;
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
