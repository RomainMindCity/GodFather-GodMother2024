using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class DoorGenerator : MonoBehaviour
{



    SpriteRenderer _spriteRenderer;
    CircleCollider2D _circleCollider;

    [SerializeField] List<Generator> _generators;

    [SerializeField] bool _normal = false;

    int _toDesactivate = 0;

    public event Action OnDoorOpened;


    void Start()
    {

        if (_generators.Count == 0) { 
            Debug.LogError("No generators assigned to the door generator", this);
            Desactivate();
            return;
        }

        _circleCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.color = Color.grey.WithAlpha(0.2f);

        if (_normal && _generators.Count > 1)
        {
            _toDesactivate = _generators.Count - 1;
        } else
        {
            _toDesactivate = 1;
        }

        for (int i = 0; i < _generators.Count; i++)
        {
            _generators[i].OnFinishedGenerator += Desactivate;
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _toDesactivate <= 0)
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    void Desactivate()
    {
        Debug.Log("Desactive");
        _toDesactivate--;

        if (_toDesactivate <= 0)
        {
            OnDoorOpened?.Invoke();
            _spriteRenderer.color = Color.white;


            //gameObject.SetActive(false);

            //    _circleCollider.enabled = false;
            //    _spriteRenderer.color = Color.white;

        }
    }

}
