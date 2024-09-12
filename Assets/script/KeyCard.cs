using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource keySound;
    [SerializeField] private Collider2D Collider2D;
    private bool inTrigger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                keySound.Play();
                spriteRenderer.enabled = false;
                Collider2D.enabled = false;

                PlayerController player = GameObject.FindWithTag("player").GetComponent<PlayerController>();
                player.hasKey = true;

            }
        }
    }
    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D truc)
    {
        if (truc.tag == "player")
        {
            inTrigger = false;
        }
    }
}
