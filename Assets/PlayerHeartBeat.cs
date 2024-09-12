using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerHeartBeat : MonoBehaviour
{
    [SerializeField] private GameObject audioPrefab;
    [SerializeField] private TextMeshProUGUI _heartBeatText;
    [SerializeField] private GameObject _heartBeatImage;
    private float _heartBeat;
    [SerializeField] private float _heartBeatMax = 180;
    [SerializeField] private float _heartBeatMin = 60;

    void Start()
    {
        StartCoroutine(FindObjects());
    }

    IEnumerator FindObjects()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        float distance = 0;
        float closestDistance = float.MaxValue;
        foreach (GameObject monster in monsters)
        {
            distance = Vector3.Distance(monster.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }
        }

        float heartBeatRange = _heartBeatMax - _heartBeatMin;
        float normalizedDistance = Mathf.Clamp01(closestDistance / 10);
        _heartBeat = _heartBeatMax - (heartBeatRange * normalizedDistance);

        // Add random variation of ±5
        _heartBeat += Random.Range(-5f, 5f);

        _heartBeatText.text = _heartBeat.ToString("F0");

        // Calculate wait time based on distance
        float waitTime = Mathf.Lerp(0.5f, 1.5f, normalizedDistance);
        _heartBeatImage.transform.DOShakeScale(0.25f, 0.1f, 10, 90, false);
        GameObject audioInstance = Instantiate(audioPrefab, transform.position, Quaternion.identity);
        if (!audioInstance.GetComponent<AudioSource>().isPlaying)
        {
           //Destroy(audioInstance);
        }
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(FindObjects());
    }
}
