using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class LoadingManager : MonoBehaviour
{

    [SerializeField] Transform _objectToAnimate;

    void Start()
    {
        DOTween.Init();
        Animation0();
    }

    void Animation0()
    {
        Debug.Log("Animation0");
        Debug.Log(_objectToAnimate.name);
        _objectToAnimate.DOMoveX(2,1);
        _objectToAnimate.DOMoveY(10, 1);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
