using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CoilHead : MonsterBehavior
{

    private float _baseSpeed;
    private float _timerFlash = 0;

    private float _timeFlashed = 3;



    protected override void Init()
    {
        _speed = _baseSpeed;
    }

    

    protected override void OnPlayerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnPlayerExit(Collider other)
    {
        throw new System.NotImplementedException();
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
    }

    protected override void Flashed()
    {
        _timerFlash += Time.deltaTime;
        if (_timerFlash >= _timeFlashed)
        {
            if (_toChase != null) { _stateAI = States.CHASE; }
            else { _stateAI = States.WALKING; }

            _timerFlash = 0;
        }
    }
}
