using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private bool isLocked;
    [SerializeField] private bool doNeedCard;

    [SerializeField] private AudioClip doorSoundLocked;
    [SerializeField] private AudioClip doorSoundLockedOther;
    [SerializeField] private AudioClip doorSoundUnlocked;

    [SerializeField] private GameObject AudioSourcePrefab;

    [SerializeField] private GameObject[] lights;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject audioSourceInstance = Instantiate(AudioSourcePrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = audioSourceInstance.GetComponent<AudioSource>();

            if (isLocked)
            {
                if (doNeedCard)
                {
                    if (other.gameObject.GetComponent<PlayerController>().HasKey)
                    {
                        audioSource.clip = doorSoundUnlocked;
                        audioSource.Play();
                        
                        foreach (GameObject light in lights)
                        {
                            light.SetActive(true);
                        }

                        Destroy(this.gameObject);
                    }
                    else
                    {
                        if (Random.Range(0, 100) < 20)
                        {
                            audioSource.clip = doorSoundLocked;
                            audioSource.Play();
                        }
                        else
                        {
                            audioSource.clip = doorSoundLockedOther;
                            audioSource.Play();
                        }
                    }
                }
                else
                {
                    audioSource.clip = doorSoundLockedOther;
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.clip = doorSoundUnlocked;
                audioSource.Play();
                Destroy(this.gameObject);
            }

            Destroy(audioSourceInstance, audioSource.clip.length);
        }
    }
}
