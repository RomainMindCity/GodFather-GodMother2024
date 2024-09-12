using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Screamer : MonoBehaviour
{

    [SerializeField] bool _active;

    Image _image;

    bool _canChange = true;

    void Start()
    {
        _image = GetComponent<Image>(); 

        //_image.color.WithAlpha(0.0f);
    }

    void Update()
    {
        //Debug.Log(PlayerHeartBeat.GetHeartBeat());
        if (_active) { 
            if (PlayerHeartBeat.GetHeartBeat() > 150 && _canChange)
            {
                _canChange = false;
                //_image.DOColor(_image.color.WithAlpha(1f), 0.1f).OnComplete(
                    //() => _image.DOColor(_image.color.WithAlpha(0), 1f).OnComplete(
                        //() => _canChange = true)
                    //);
            }
            else if (PlayerHeartBeat.GetHeartBeat() < 150)
            {
                //_image.color = Color.white.WithAlpha(0);
            }
        }
    }
}
