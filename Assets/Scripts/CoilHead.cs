using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class CoilHead : MonsterBehavior
{

    [SerializeField] private float _baseSpeed = 200;

    private float _timerFlash = 0;
    private float _timeFlashed = 3;

    public Seeker seeker;

    [SerializeField]
    float radiusPatrol = 10;

    Path path;

    Vector3 _pointToGo = Vector3.zero;

    protected override void Init()
    {
        _stateAI = States.NONE;
        _speed = _baseSpeed;

        print(_speed.ToString());
        seeker = GetComponent<Seeker>();
        if (_aiPath.getPath() != null )
        {
            FindFurthestPointOnPath(_aiPath.getPath(), transform.position);
        }
    }

    

    protected override void OnPlayerEnter(Collider2D other)
    {

        //Debug.Log("Entered");

        

        if (other.gameObject.tag == "Player") {

            if (checkWalls(other))
            {
                return;
            }
            _toChase = other.gameObject.transform;
            _stateAI = States.CHASE;
        }
    }

    protected override void OnPlayerExit(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {


            if (checkWalls(other))
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
            _pointToGo = transform.position + RotateVector2D(new Vector3(radiusPatrol, 0, 0), Random.Range(0f,360f));
        }
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
        _timerFlash += Time.deltaTime;
        if (_timerFlash >= _timeFlashed)
        {
            if (_toChase != null) { _stateAI = States.CHASE; }
            else { _stateAI = States.WALKING; }

            _timerFlash = 0;
            _aiPath.maxSpeed = _speed;
        }
    }

    public override void FlashMonster(Vector3? playerPosition = null)
    {
        _stateAI = States.FLASHED;
        _timerFlash = 0;
        _aiPath.maxSpeed = 0;



        // DEBUG 
        Debug.Log("Coilhead Flashed ! ");
    }


    Vector3 FindFurthestPointOnPath(Path path, Vector3 startPosition)
    {
        Vector3 furthestPoint = Vector3.zero;
        float maxDistance = 0f;

        // Iterate through the path's vector points
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
