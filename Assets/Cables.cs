using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cables : MonoBehaviour
{

    List<Transform> _cables = new List<Transform>();

    [SerializeField] bool _vertical = false;

    void Start()
    {
        _cables.AddRange(GetComponentsInChildren<Transform>());
        //int i = 1;
        //foreach (Transform cable in _cables)
        //{
        //    i++;
        //    cable.DOLocalMoveX(0.01f * i, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        //}
        if ( !_vertical) { 
            _cables[0].DOMoveX(transform.position.x - Random.Range(2,7), Random.Range(1.5f,3)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            _cables[1].DOMoveX(transform.position.x - Random.Range(9, 13), Random.Range(1.5f, 3)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
        else
        {
            _cables[0].DOMoveY(transform.position.y - Random.Range(2, 7), Random.Range(1.5f, 3)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            _cables[1].DOMoveY(transform.position.y - Random.Range(9, 13), Random.Range(1.5f, 3)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }

}
