using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Panic : MonoBehaviour
{

    List<GameObject> _list = new List<GameObject>();

    [SerializeField] bool iheuiahe;

    [SerializeField] AnimationCurve curve;

    public static bool end = false;

    


    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _list.Add(transform.GetChild(i).gameObject);
        }

    }

    void ChangeColor(float value)
    {
        foreach (var item in _list)
        {
            item.transform.GetChild(0).GetComponent<Image>().color = Color.white.WithAlpha(value);
            item.transform.GetChild(1).GetComponent<Image>().color = Color.white.WithAlpha(value);
        }
    }

    public void ChangeNothing()
    {
        foreach (var item in _list)
        {
            item.transform.GetChild(0).GetComponent<Image>().DOColor(Color.white.WithAlpha(0), 1);
            item.transform.GetChild(1).GetComponent<Image>().DOColor(Color.white.WithAlpha(0), 1);
        }
    }

    void Update()
    {

        if (end)
        {
            ChangeNothing(); 
            return;
        }

        float per = PlayerHeartBeat.GetHeartBeat() / 180;

        //Debug.Log(per);

        float value = curve.Evaluate(per);

        //float value = Mathf.Clamp(PlayerHeartBeat.GetHeartBeat() / 400, 0, 1);

        ChangeColor(value);

    }
}
