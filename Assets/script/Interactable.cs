using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private AudioSource doorsSound;
    [SerializeField] Collider2D Collider;
    [SerializeField] private bool etat;
    [SerializeField] Collider2D detector;
    void Start()
    {
        Collider.enabled = true;
        etat = false;
        etat = detector;
    }


    void Update()
    {
        if (etat)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider.enabled = false;
                detector.enabled = false;
                transform.position = transform.position + new Vector3(1f, 1, 0);
                transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, 90f);
                Debug.Log("porte ouverte");
            }

        }

    }

    void OnTriggerEnter2D(Collider2D truc)
    {
        if (truc.tag == "player")
        {
            etat = true;
        }
    }

    void OnTriggerExit2D(Collider2D truc)
    {
        if (truc.tag == "player")
        {
            etat = false;
        }
    }



}

