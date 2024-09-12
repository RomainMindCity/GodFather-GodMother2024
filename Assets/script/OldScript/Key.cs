using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField]private GameObject porte; // Il faudra drag'n drop la "porte" que l'on souhaite ouvrir dans cette variable (depuis l'éditeur)
    private bool inTrigger;
    [SerializeField]private AudioSource keySound;
    [SerializeField] private int probability;
    [SerializeField] private Collider2D Collider2D;

    void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                keySound.Play();
                Collider2D.enabled = false;
                porte.GetComponent<door>().ouverture();
                spriteRenderer.enabled = false;

            }
        }
    }

    // Tant que le joueur reste dans ce trigger, si il appuis sur la touche "E" du clavier, ça lance la fonction "ouverture" qui se trouve sur cette porte
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
