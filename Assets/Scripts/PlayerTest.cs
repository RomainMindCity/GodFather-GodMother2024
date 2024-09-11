using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{


    Vector2 _velocity;

    float _speed = 5;

    void Start()
    {

    }

    void Update()
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
}
