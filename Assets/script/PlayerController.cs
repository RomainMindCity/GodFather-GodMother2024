using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _movSpeed;
    private float _SpeedX, _SpeedY;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        _SpeedX = Input.GetAxisRaw("Horizontal") * _movSpeed;
        _SpeedY = Input.GetAxisRaw("Vertical") * _movSpeed;
        rb.velocity = new Vector2(_SpeedX, _SpeedY);
    }

}
