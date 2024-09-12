using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QteAll : MonoBehaviour
{

    bool _active;

    public bool IsActive => _active;

    bool _finished;

    bool success = false;

    Scrollbar _scrollBar;
    Image _imageBackScroll;

    [SerializeField] GameObject _handle;

    [SerializeField] QteRed _qteRed;

    GameObject _qteYellow;

    [SerializeField] float _pixelPerFrame = 0.0001f;

    public Generator _gen;

    private void Start()
    {
        _scrollBar = GetComponent<Scrollbar>();
        _imageBackScroll = GetComponent<Image>();
        _qteYellow = transform.Find("Image2").gameObject;
        //Activate();
        DesacStart();
    }

    public void DesacStart()
    {
        _imageBackScroll.color = Color.clear;
        _handle.GetComponent<Image>().color = Color.clear;
        _qteRed.gameObject.SetActive(false);
        _qteYellow.SetActive(false);
    }

    public void Activate() 
    {
        _finished = false;

        _qteRed.gameObject.SetActive(true);
        _qteYellow.SetActive(true);
        _imageBackScroll.DOColor(Color.white, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            
            _scrollBar.value = 0;
            
            _qteRed._active = true;
            _handle.GetComponent<Image>().DOColor(Color.white, 0.1f).SetEase(Ease.InOutSine);

            _active = true;
        });

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    Activate();
        //}

        if (!_active) return;

        if (_scrollBar.value >= 1)
        {
            _active = false;
            _finished = true;
            _qteRed._active = true;


            if (_gen != null)
            {
                _gen.AddFuel(-15);
            }
            

            _imageBackScroll.DOColor(Color.clear, 0.5f).SetEase(Ease.InOutSine);
            _qteRed.gameObject.SetActive(false);
            _qteYellow.SetActive(false);
            _handle.GetComponent<Image>().DOColor(Color.clear, 0.2f).SetEase(Ease.InOutSine);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _active = false;
            _finished = true;
            _qteRed._active = true;

            //print(_scrollBar.value);
            if (_scrollBar.value > 0.40 && _scrollBar.value < 0.45)
            {
                if (_gen != null)
                {
                    _gen.AddFuel(10);
                }
            }
            else if (_scrollBar.value >= 0.45 && _scrollBar.value < 0.668)
            {
                //Debug.Log("Perfect");
                if (_gen != null)
                {
                    _gen.AddFuel(5);
                }
            }
            else
            {
                //Debug.Log("Fail");
                if (_gen != null)
                {
                    _gen.AddFuel(-10);
                }
            }

            //if (_qteRed.Perfect)
            //{
            //    Debug.Log("Perfect");
            //}
            //else
            //{
            //    Debug.Log("Fail");
            //}

            _imageBackScroll.DOColor(Color.clear, 0.5f).SetEase(Ease.InOutSine);
            _qteRed.gameObject.SetActive(false);
            _qteYellow.SetActive(false);
            _handle.GetComponent<Image>().DOColor(Color.clear, 0.2f).SetEase(Ease.InOutSine);
        }

        _scrollBar.value += _pixelPerFrame;
    }
}
