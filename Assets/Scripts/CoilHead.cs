using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


/// <summary>
/// Coilhead is a monster that will stop when flashed
/// </summary>
public class CoilHead : MonsterBehavior
{
    [SerializeField] private float _baseSpeed = 200;

    public Seeker seeker;

    [SerializeField]
    float radiusPatrol = 10;

    float _timerFlash = 0;
    float _timeFlashed = 0.5f;

    Path path;

    Vector3 _pointToGo = Vector3.zero;

    protected override void Init()
    {
        _stateAI = States.NONE;
        _speed = _baseSpeed;

        _canBeControlled = true;

        print(_speed.ToString());
        seeker = GetComponent<Seeker>();
        if (_aiPath.getPath() != null )
        {
            FindFurthestPointOnPath(_aiPath.getPath(), transform.position);
        }
    }

    

    protected override void OnPlayerEnter(Collider2D other)
    {

        Debug.Log("Entered");

        

        if (other.gameObject.tag == "Player") {
            
            _toChase = other.gameObject.transform;

            if (_needToSee && checkWalls(other))
            {
                return;
            }

            _stateAI = States.CHASE;
        }
    }

    protected override void OnPlayerExit(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {


            if (_needToSee && checkWalls(other))
            {
                return;
            }

            _toChase = null;
        }
    }

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

        transform.Translate(_velocity * _speed * Time.deltaTime);
    }


    // ------------ AI BEHAVIORS ------------

    protected override void None()
    {
        _stateAI = States.WALKING;
        _speed = _baseSpeed;
        _aiPath.maxSpeed = _speed;
    }
    protected override void Walking()
    {
        _aiPath.destination = _pointToGo;
        

        if (_aiPath.reachedEndOfPath)
        {
            _pointToGo = transform.position + RotateVector2D(new Vector3(radiusPatrol, 0, 0), Random.Range(0f,360f));
        }

        if (_toChase != null && checkWalls(_toChase)) { _stateAI = States.CHASE; }
    }

    

    protected override void Chase()
    {
        //Debug.Log("Chase ? ");
        if (_toChase == null && _aiPath.reachedEndOfPath) { _stateAI = States.WALKING; return; }
        _aiPath.maxSpeed = _speed;
        if (_toChase != null)
        {
            _aiPath.destination = _toChase.position;
        }
        //Debug.Log("Chase");
    }

    protected override void Flashed()
    {
    }

    public override void FlashMonster(Vector3? playerPosition = null)
    {

        if (_state == StatesBehavior.NONE && _activatedByFlash)
        {
            _state = StatesBehavior.AI;
            return;
        }

        _stateAI = States.FLASHED;
        //_timerFlash = 0;
        _aiPath.maxSpeed = 0;




        // DEBUG 
        Debug.Log("Coilhead Flashed ! ");
    }

    public override void UnflashMonster()
    {
        StartCoroutine(_afterUnflash());
    }

    IEnumerator _afterUnflash()
    {
        yield return new WaitForSeconds(_timeFlashed);
        _aiPath.maxSpeed = _speed;
        if (_toChase != null)
        {
            _stateAI = States.CHASE;
        }
        else
        {
            _stateAI = States.WALKING;
        }
    }

    Vector3 FindFurthestPointOnPath(Path path, Vector3 startPosition)
    {
        Vector3 furthestPoint = Vector3.zero;
        float maxDistance = 0f;

        for (int i = 0; i < path.vectorPath.Count; i++)
        {
            float distance = Vector3.Distance(startPosition, path.vectorPath[i]);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestPoint = path.vectorPath[i];
            }
        }
        print(furthestPoint);
        return furthestPoint;
    }

    
}
