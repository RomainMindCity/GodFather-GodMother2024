using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Light2D = UnityEngine.Rendering.Universal.Light2D;

public class RaimbowLight : MonoBehaviour
{
    [SerializeField] private Vector3 color;

    [SerializeField] private Vector2 _paramsPlus;

    [SerializeField] private Vector3 _paramsMul;

    [SerializeField] private Light2D light2D;
    void Start()
    {
        StartCoroutine(Raimbow());
    }

    IEnumerator Raimbow()
    {
        while (true)
        {
            RaimbowColor();
            yield return new WaitForSeconds(0f);
        }
    }

    void RaimbowColor()
    {
        
        color.x = Mathf.Abs(Mathf.Sin((color.x + Time.time) * _paramsMul.x));
        color.y = Mathf.Abs(Mathf.Sin((color.y + Time.time) * _paramsMul.y + _paramsPlus.x));
        color.z = Mathf.Abs(Mathf.Sin((color.z + Time.time) * _paramsMul.z + _paramsPlus.y));

        float t = Mathf.PingPong(Time.time, 1) / 1;
        // light2D.color = Color.HSVToRGB(t, t, t);
        light2D.color = new Color(color.x, color.y, color.z);
    }
}
