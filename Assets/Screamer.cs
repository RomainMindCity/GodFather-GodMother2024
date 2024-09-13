using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Screamer : MonoBehaviour
{

    [SerializeField] bool _active;

    [SerializeField] float _timeToScream = 1.5f;
    float _timerScream = 0;

    bool _scream = false;

    AudioSource _audioSource;

    Image _image;

    bool _canChange = true;

    Transform _replayButton;

    Transform _menuButton;

    void Start()
    {
        _replayButton = transform.Find("ReplayButton");
        _menuButton = transform.Find("MenuButton");

        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();

        _replayButton.gameObject.SetActive(false);
        _menuButton.gameObject.SetActive(false);

        _image.color.WithAlpha(0.0f);
    }

    IEnumerator _afterScream()
    {

        Debug.Log("SCREAM COROUTINE START");

        yield return new WaitForSeconds(1.0f);

        Debug.Log("SCREAM COROUTINE END");

        _image.DOColor(Color.white.WithAlpha(1), 1f);

        _replayButton.gameObject.SetActive(true);
        _menuButton.gameObject.SetActive(true);



        //SceneManager.LoadScene("LoadingScene");
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    void Update()
    {
        //Debug.Log(PlayerHeartBeat.GetHeartBeat());
        if (_active) { 

            if (_scream)
            {
                _timerScream += Time.deltaTime;
                if (_timerScream >= _timeToScream)
                {
                    _timerScream = 0;
                    _scream = false;
                    _image.DOColor(Color.black, 0.1f);


                    StartCoroutine(_afterScream());

                    // RENVOYER AU MENU
                }
            }

            if (PlayerHeartBeat.GetHeartBeat() > 160 && _canChange)
            {
                _canChange = false;
                _image.DOColor(_image.color.WithAlpha(1), 0.1f);
                _scream = true;
                _audioSource.Play();
                
                // JOUE LE SON

                //_image.DOColor(_image.color.WithAlpha(0.5f), 0.2f).OnComplete(
                //    () => _image.DOColor(_image.color.WithAlpha(0), 1f).OnComplete(
                //        () => _canChange = true)
                //    );
            }
            else if (PlayerHeartBeat.GetHeartBeat() < 150)
            {
                //_image.color = Color.white.WithAlpha(0);
            }
        }
    }
}
