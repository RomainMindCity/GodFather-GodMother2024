using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoors : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceA;
    [SerializeField] AudioSource audioSourceB;
    public int probability;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
