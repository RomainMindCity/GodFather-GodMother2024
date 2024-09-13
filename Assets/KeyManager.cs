using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private AudioClip keySound;
    [SerializeField] private GameObject AudioSourcePrefab;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject audioSourceInstance = Instantiate(AudioSourcePrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = audioSourceInstance.GetComponent<AudioSource>();
            audioSource.clip = keySound;
            audioSource.Play();
            other.gameObject.GetComponentInParent<PlayerController>().HasKey = true;
            Destroy(this.gameObject);
            Destroy(audioSourceInstance, audioSource.clip.length);
        }   
    }
}
