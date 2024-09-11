using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wolf is a monster that will rush to the player at high speed and stop.
/// </summary>
public class Wolf : MonsterBehavior
{

    [SerializeField] float _timeAfterChase = 3;

    protected override void Init()
    {
        _stateAI = States.NONE;
        _speed = 10;

    }

    protected override void OnPlayerEnter(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("WOLF ENTERED");
            _toChase = other.gameObject.transform;
        }
    }

    protected override void OnPlayerExit(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _toChase = null;
        }
    }


    public override void FlashMonster(Vector3? playerPosition = null)
    {
        
    }

    public override void UnflashMonster()
    {
        
    }
    protected override void Player()
    {
        
    }

    //------------------ AI STATES ------------------

    protected override void Flashed()
    {
        _stateAI = States.NONE;
    }

    protected override void Walking()
    {
        
    }

    protected override void None()
    {


        _aiPath.canMove = false;
        if (_toChase != null && !checkWalls(_toChase))
        {
            _stateAI = States.CHASE;
            _aiPath.destination = _toChase.position;
            _aiPath.maxSpeed = _speed;
            _aiPath.canMove = true;
        }

    }

    IEnumerator _afterChase()
    {
        _stateAI = States.WALKING;

        Debug.Log("WOLF START COROUTINE");
        Debug.Log(_timeAfterChase.ToString());

        yield return new WaitForSeconds(_timeAfterChase);

        Debug.Log("WOLF END COROUTINE");

        if (_toChase != null)
        {
            Debug.Log("WOLF CHASE");
            _aiPath.destination = _toChase.position;
            _stateAI = States.CHASE;
        }
        else
        {
            Debug.Log("WOLF NONE");
            _stateAI = States.NONE;
        }
    }

    protected override void Chase()
    {
        if (_aiPath.reachedEndOfPath) 
        {
            StartCoroutine(_afterChase());
        }
    }


}
