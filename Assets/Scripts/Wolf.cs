using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Wolf is a monster that will rush to the player at high speed and stop.
/// </summary>
public class Wolf : MonsterBehavior
{

    [SerializeField] bool _animateFun = false;

    [SerializeField] float _timeAfterChase = 3;

    [SerializeField] float _funDistance;

    protected override void Init()
    {
        _stateAI = States.WALKING;
        _speed = 10;

       _aiPath.maxSpeed = _speed;

        if (_animateFun)
        {
            _aiPath.canMove = true;
            _aiPath.destination = transform.position + RotateVector2D(new Vector3(0, 3f + _funDistance, 0), transform.eulerAngles.z + 30);
        }
    }

    void AfterAnim()
    {
        Vector3 targetPosition = transform.position + RotateVector2D(new Vector3(0, 1f + _funDistance, 0), transform.eulerAngles.z + 30);
        Debug.Log("POSITION");
        Debug.Log(targetPosition);

        transform.DOMove(targetPosition, 0.5f).OnComplete(AfterAnim);
    }

    void AfterAnimRotate()
    {

        Vector3 initialMove = new Vector3(0, 3f, 0);
        float initialRotationZ = transform.eulerAngles.z + 30;


        transform.DORotate(new Vector3(0,0, initialRotationZ), 0.5f).OnComplete(AfterAnimRotate);
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
        if (_state == StatesBehavior.NONE && _activatedByFlash)
        {
            _state = StatesBehavior.AI;
            return;
        }
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
        if (!_animateFun) return;

        _aiPath.maxSpeed = _speed;

        if (_aiPath.reachedEndOfPath)
        {
            _aiPath.destination = transform.position + RotateVector2D(new Vector3(0, 3f + _funDistance, 0), transform.eulerAngles.z + 30);
        }

        //_aiPath.maxSpeed = _speed;
        ////_aiPath.destination = transform.position + RotateVector2D(new Vector3(0, 0.8f, 0), transform.rotation.z - 90);

        //if (_aiPath.reachedEndOfPath)
        //{
        //    Debug.Log("WOLF REACHED END OF PATH");
        //    //_aiPath.destination = transform.position + RotateVector2D(new Vector3(0,3f,0), transform.rotation.z - 30);
        //}
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
