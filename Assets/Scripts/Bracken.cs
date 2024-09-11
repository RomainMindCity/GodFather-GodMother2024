using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Bracken will just go back when flashed
/// </summary>
public class Bracken : MonsterBehavior
{


    [SerializeField] private float _baseSpeed = 200;
    [SerializeField] private float _angrySpeed = 300;
    [SerializeField] private float _distToRun = 10;

    private float _timerFlash = 0;
    private float _timeFlashed = 3;

    [SerializeField]
    float radiusPatrol;

    Vector3 _pointToGo = Vector3.zero;

    protected override void Init()
    {
        _stateAI = States.NONE;
        _speed = _baseSpeed;

    }



    protected override void OnPlayerEnter(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

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
        throw new System.NotImplementedException();
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
            _pointToGo = transform.position + RotateVector2D(new Vector3(radiusPatrol, 0, 0), Random.Range(0f, 360f));
        }

        if (_toChase != null && checkWalls(_toChase)) { _stateAI = States.CHASE; }
    }
    protected override void Chase()
    {
        if (_toChase == null && _aiPath.reachedEndOfPath) { _stateAI = States.WALKING; return; }
        _aiPath.maxSpeed = _speed;
        if (_toChase != null)
        {
            _aiPath.destination = _toChase.position;
        }
    }

    protected override void Flashed()
    {
        Walking();
        _timerFlash += Time.deltaTime;
        
        if (_timerFlash >= _timeFlashed)
        {
            if (_toChase != null) { _stateAI = States.CHASE; }
            else { _stateAI = States.WALKING; }

            _timerFlash = 0;
            _aiPath.maxSpeed = _speed;
        }
        
    }

    public override void UnflashMonster() { }

    public override void FlashMonster(Vector3? playerPosition = null)
    {

        if (_stateAI == States.FLASHED)
        {
            _stateAI = States.CHASE;
            _speed = _angrySpeed;
            _timerFlash = 0;
            _aiPath.maxSpeed = _speed;
        } else
        {
            _stateAI = States.FLASHED;
            _timerFlash = 0;
            _speed = _angrySpeed;
            _aiPath.maxSpeed = _speed;

            if (_toChase != null)
            {
                Vector2 vec1 = new Vector2(transform.position.x, transform.position.y);
                Vector2 vec2 = new Vector2(_toChase.position.x, _toChase.position.y);

                float distance = Vector2.Distance(vec1, vec2);
             
                _aiPath.destination = -_aiPath.destination * (_distToRun / distance);
            }
            else
            {
                _aiPath.destination = -_aiPath.destination * _distToRun / 2;
            }
    }


        // DEBUG 
        Debug.Log("Bracken Flashed ! ");
    }

    Vector3 RotateVector2D(Vector3 vector, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        float rotatedX = vector.x * cos - vector.y * sin;
        float rotatedY = vector.x * sin + vector.y * cos;

        return new Vector3(rotatedX, rotatedY, vector.z);
    }
}


