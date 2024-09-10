using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CoilHead : MonsterBehavior
{

    [SerializeField] private float _baseSpeed = 200;

    private float _timerFlash = 0;
    private float _timeFlashed = 3;



    protected override void Init()
    {
        _stateAI = States.CHASE;
        _speed = _baseSpeed;

        print(_speed.ToString());

    }

    

    protected override void OnPlayerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            _toChase = other.gameObject.transform; 
        }
    }

    protected override void OnPlayerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
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
        throw new System.NotImplementedException();
    }
    protected override void Walking()
    {
        _aiPath.destination = transform.position + new Vector3(10,10,10);
    }
    protected override void Chase()
    {
        _aiPath.maxSpeed = _speed;
        _aiPath.destination = _toChase.position;
        //Debug.Log("Chase");
    }

    protected override void Flashed()
    {
        _timerFlash += Time.deltaTime;
        if (_timerFlash >= _timeFlashed)
        {
            if (_toChase != null) { _stateAI = States.CHASE; }
            else { _stateAI = States.WALKING; }

            _timerFlash = 0;
            _aiPath.maxSpeed = _speed;
        }
    }

    public override void FlashMonster()
    {
        _stateAI = States.FLASHED;
        _timerFlash = 0;
        _aiPath.maxSpeed = 0;

        // DEBUG 
        Debug.Log("Coilhead Flashed ! ");
    }
}
