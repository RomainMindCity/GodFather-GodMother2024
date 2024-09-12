using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIEnd : MonoBehaviour
{

    [SerializeField] DoorGenerator _door;

    Transform _text; 

    void Start()
    {
        _text = transform.Find("Text");

        _text.gameObject.SetActive(false);

        _door.OnDoorOpened += () =>
        {
            //Debug.Log("");
            _text.gameObject.SetActive(true);
        };
    }
}
