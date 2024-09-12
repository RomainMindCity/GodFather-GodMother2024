using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorGenerator : MonoBehaviour
{

    SpriteRenderer _spriteRenderer;
    CircleCollider2D _circleCollider;

    [SerializeField] Generator _generator;

    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.color = Color.grey.WithAlpha(0.2f);

        _generator.OnFinishedGenerator += Desactivate;


    }

    void Desactivate()
    {
        _circleCollider.enabled = false;
        _spriteRenderer.color = Color.white;
    }

}
