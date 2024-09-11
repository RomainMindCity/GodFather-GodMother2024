using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource doorsSound;
    [SerializeField] Collider2D Collider;
    [SerializeField] private bool etat ;
    [SerializeField] Collider2D detector;
    void Start()
    {
        spriteRenderer.enabled =true;
        Collider.enabled = true;
        etat = false;
    }


    void Update()
    {
        if (etat)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorsSound.Play();
                spriteRenderer.enabled = false;
                Collider.enabled = false;
                detector.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            etat = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            etat = false;
        }
    }



}

