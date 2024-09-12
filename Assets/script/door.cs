using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class door : MonoBehaviour
{
    // Ce script se met sur la porte qui devra s'ouvrir
    // ATTENTION il marche en binome avec un autre script : le script "levier"

    public float speed = 2f;                                            // Vitesse d'ouverture de la porte
    [SerializeField, Range(0.1f, 50f)] private float destination = 5f;  // distance entre la porte et sa destination une fois ouverte (limité entre 0.1 et 50)
    [SerializeField, Range(0f, 360f)] private float RotationPath;       // Permet de faire pivoter le trajet de la porte
    private Vector2 directionAngle;                                     // Variable pour tranformer l'angle RotationPath (en degré) vers une direction (Vector2)
    private Vector3 destinationPosition;                                // Sert a transformer la distance avec la destination en coordonnées X/Y/Z
    private bool go;
    public int probability;
    [SerializeField] AudioSource audioSourceA;
    [SerializeField] AudioSource audioSourceB;

    // Ici on va enregistrer la position de la destination en utilisant l'angle de rotation et la distance qu'on a choisie
    void Start()
    {
        directionAngle = (Vector2)(Quaternion.Euler(0, 0, RotationPath) * Vector2.right);
        destinationPosition = transform.position + (Vector3)directionAngle * destination;
    }

    // Si go est vrai alors on déplace la porte vers sa destination
    void Update()
    {
        if (go)
        {
            transform.position = Vector2.MoveTowards(transform.position, destinationPosition, speed * Time.deltaTime);
        }
    }

    // fontion pour passer "GO" en vrai, qui doit être appeler depuis un autre script (Comme sur un levier qui doit ouvrire cette porte, CF script levierPorte)
    public void ouverture()
    {
        StartCoroutine(cooldown());
        go = true;
       
    }

    // Fonction pour dessiner le chemin que prendra la porte en s'ouvrant dans l'éditeur
    void OnDrawGizmos()
    {
        if (!Application.IsPlaying(gameObject))
        {
            directionAngle = (Vector2)(Quaternion.Euler(0, 0, RotationPath) * Vector2.right);
            destinationPosition = transform.position + (Vector3)directionAngle * destination;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destinationPosition, 0.2f);
        Gizmos.DrawLine(destinationPosition, transform.position);
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        probability = Random.Range(0, 101);
        if (probability <= 20)
        {
            audioSourceA.Play();
        }
        else
        {
            audioSourceB.Play();
        }
    }
}
