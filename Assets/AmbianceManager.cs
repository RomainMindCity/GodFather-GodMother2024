using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceManager : MonoBehaviour
{
    [Tooltip("Time in seconds before activating the ambiance")]
    [SerializeField] private float time;
    void Start()
    {
       StartCoroutine(ActivateAmbiance()); 
    }

    IEnumerator ActivateAmbiance()
    {
        yield return new WaitForSeconds(time);
        //make a fade in effect
        for (float i = 0; i < 100; i++)
        {
            GetComponent<AudioSource>().volume = i/100;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
