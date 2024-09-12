using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoors : MonoBehaviour
{
    [SerializeField] AudioSource Sound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sound.Play();
    }


}
