using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Generator : MonoBehaviour
{

    bool _isPlayer = false;

    [SerializeField] float _maxFuel = 1000;

    [SerializeField] float _fuelPerFrame = 1f;

    float _fuel = 0;

    bool _finished => _fuel >= _maxFuel;

    UnityEngine.UI.Slider _slider;

    public event Action OnFinishedGenerator;


    void Start()
    {
        _slider = GetComponentsInChildren<UnityEngine.UI.Slider>()[0];
        if (_slider != null)
        {
            _slider.maxValue = _maxFuel;
            _slider.value = _fuel;
        }

    }


    //bool IsFinished()
    //{
    //    return _fuel >= _maxFuel;
    //}

    private void FixedUpdate()
    {
        if (_finished) return;

        if (_isPlayer)
        {
            _fuel += _fuelPerFrame;

            //Debug.Log("CHANGIN");

            if (_slider != null )
            {
                _slider.value = _fuel;
            }

            if (_fuel >= _maxFuel)
            {
                _slider.gameObject.SetActive(false);
                OnFinishedGenerator?.Invoke();

                // Pour listen -> get le generator et faire generator.FinishedGenerator += LaFonction;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_finished) return;

        if (collision.CompareTag("Player"))
        {
            _isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_finished) return;

        if (collision.CompareTag("Player"))
        {
            _isPlayer = false;
        }
    }
}
