using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmbianceSound : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(cooldownSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator cooldownSound()
    {
        yield return new WaitForSeconds(10f);
        audioSource.Play();
    }
}
