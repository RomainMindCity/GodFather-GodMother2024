using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cables : MonoBehaviour
{

    List<Transform> _cables = new List<Transform>();

    void Start()
    {
        _cables.AddRange(GetComponentsInChildren<Transform>());
        //int i = 1;
        //foreach (Transform cable in _cables)
        //{
        //    i++;
        //    cable.DOLocalMoveX(0.01f * i, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        //}

        _cables[0].DOMoveX(transform.position.x - 5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        _cables[1].DOMoveX(transform.position.x - 10f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

}
