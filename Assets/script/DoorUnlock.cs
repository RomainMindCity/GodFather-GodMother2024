using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    [SerializeField]private bool inTrigger;
    public AudioSource doorSound;
    [SerializeField]private SpriteRenderer spriteRenderer;


    void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerController player = GameObject.FindWithTag("player").GetComponent<PlayerController>();
                if (player.hasKey)
                {
                    OpenDoor();
                    spriteRenderer.enabled = false;
                }
                else
                {
                    Debug.Log("besoin d'une clé.");
                }
            }
        } 
           
    }

    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.CompareTag("player"))
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D truc)
    {
        if (truc.CompareTag("player"))
        {
            inTrigger = false;
        }
    }

    void OpenDoor()
    {
        doorSound.Play();
        GetComponent<Collider2D>().enabled = false;
    }
}
