using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QteRed : MonoBehaviour
{

    public bool _active;

    bool _isInRed = false;

    public bool Perfect => _isInRed;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        print(collision.gameObject.name);

        if (!_active) return;

        if (collision.gameObject.tag == "Qte")
        {
            _isInRed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_active) return;

        if (collision.gameObject.tag == "Qte")
        {
            _isInRed = false;
        }
    }

}
