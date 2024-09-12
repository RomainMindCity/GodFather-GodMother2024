using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
public class LoadingManager : MonoBehaviour
{


    GameObject _playButton;
    GameObject _optionButton;
    GameObject _quitButton;

    GameObject _playButtonBack;
    GameObject _optionButtonBack;
    GameObject _quitButtonBack;

    GameObject _center;

    List<GameObject> _listButtons = new List<GameObject>();

    List<GameObject> _listButtonsBackgrounds = new List<GameObject>();

    Color _endColor = Color.red;
    Color _startColor = Color.grey;

    int _selectedButton = 0;

    bool canChange = true;

    void Start()
    {
        DOTween.Init();

        _center = GameObject.Find("Center");
        _playButton = GameObject.Find("Play");
        _optionButton = GameObject.Find("Options");
        _quitButton = GameObject.Find("Quit");

        _playButtonBack = GameObject.Find("PlayBack");
        _optionButtonBack = GameObject.Find("OptionsBack");
        _quitButtonBack = GameObject.Find("QuitBack");

        _listButtons.Add(_playButton);
        _listButtons.Add(_optionButton);
        _listButtons.Add(_quitButton);

        _listButtonsBackgrounds.Add(_playButtonBack);
        _listButtonsBackgrounds.Add(_optionButtonBack);
        _listButtonsBackgrounds.Add(_quitButtonBack);


        ChangeColors();
        ChangePositionButton();
    }

    void AnimationGoLeft(GameObject gameObject, int i, float moreSize = 0)
    {
        float duration = 0.3f;

        
        gameObject.transform.DOScale(new Vector2(1.0f, 1.0f), duration);
        gameObject.transform.DORotate(new Vector3(0, 0, 0), duration);
        gameObject.transform.DOMove(new Vector2(_center.transform.position.x/2f , _center.transform.position.y - (i * 20) - moreSize), duration).SetEase(Ease.OutCubic);
    
    }


    void ChangeState()
    {
        canChange = true;
    }

    void AnimationGoCenter(GameObject gameObject, int i, float moreSize = 0)
    {
        //.SetLoops(-1) to infinite

        float duration = 0.3f;

        gameObject.transform.DOScale(new Vector2(1.5f, 1.5f), duration);
        gameObject.transform.DORotate(new Vector3(0, 0, 0), duration);
        gameObject.transform.DOMove(new Vector2(_center.transform.position.x, _center.transform.position.y - (i * 20) - moreSize), duration).SetEase(Ease.OutCubic);
    
        
    }

    int i = 1;
    void AnimationCenterCubes(GameObject gameObject)
    {
        foreach (Transform cube in gameObject.GetComponentsInChildren<Transform>())
        {

            int currentIndex = i;

            Transform currentCube = cube;

            currentCube.DORotate(new Vector3(0, 0, 360), 0.8f,RotateMode.FastBeyond360).OnComplete(() =>
                {
                    if (currentCube != gameObject.transform)
                    { 
                        if (currentIndex % 2 == 0)
                        {
                            currentCube.DOScale(new Vector2(1.5f,1.5f), 3f).SetLoops(-1, LoopType.Yoyo);
                            currentCube.DORotate(new Vector3(0, 0, -360), 3.5f, RotateMode.FastBeyond360).SetLoops(-1);
                        }
                        else
                        {
                            currentCube.DORotate(new Vector3(0, 0, -360), 2.5f, RotateMode.FastBeyond360).SetLoops(-1);
                        }
                    }
                });
            
            i++;
            
        }
    }



    private void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSelectedButton(+1);
            ChangeColors();
            ChangePositionButton();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelectedButton(-1);
            ChangeColors();
            ChangePositionButton();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            DoActionButton();
        }

    }

    void ChangePositionButton()
    {
        for (int i = 0; i < _listButtons.Count; i++)
        {
            if (i == _selectedButton)
            {
                AnimationGoCenter(_listButtons[i], i);
                AnimationGoCenter(_listButtonsBackgrounds[i], i, 40);
                AnimationCenterCubes(_listButtonsBackgrounds[i]);
            }
            else
            {
                AnimationGoLeft(_listButtons[i], i);
                AnimationGoLeft(_listButtonsBackgrounds[i], i, 20);
            }
        }
    }

    void DoActionButton()
    {
        switch(_selectedButton)
        {
            case 0:
                Play();
                break;
            case 1:
                Options();
                break;
            case 2:
                Quit();
                break;
        }
    }

    void Play()
    {
        _playButton.GetComponent<TextMeshProUGUI>().DOColor(Color.black, 0.2f);
        SceneManager.LoadScene("MainScene");
    }

    void Options()
    {
        throw new System.NotImplementedException();
    }

    public void Quit()
    {
        Application.Quit();
    }

    void ChangeSelectedButton(int i)
    {
        _selectedButton += i;
        if (_selectedButton < 0)
        {
            _selectedButton = _listButtons.Count - 1;
        }
        else if (_selectedButton >= _listButtons.Count)
        {
            _selectedButton = 0;
        }
    }

    void ChangeColors()
    {
        for (int i = 0; i < _listButtons.Count; i++)
        {
            if (i == _selectedButton)
            {
                _listButtons[i].GetComponent<TextMeshProUGUI>().DOColor(Color.red, 0.2f);
                foreach (var item in _listButtonsBackgrounds)
                {
                    List<Image> images = new List<Image>(item.GetComponentsInChildren<Image>());
                    images[0].DOColor(Color.grey.WithAlpha(1f), 0.2f);//.SetLoops(-1);
                    //images[0].DOFade(1, 0.2f);
                    images[1].DOColor(Color.black.WithAlpha(1f), 0.2f);//.SetLoops(-1);
                    //images[1].DOFade(1, 0.2f);
                }
            }
            else
            {
                _listButtons[i].GetComponent<TextMeshProUGUI>().DOColor(Color.white, 0.2f);
                foreach (var item in _listButtonsBackgrounds[i].GetComponentsInChildren<Image>())
                {
                    item.DOColor(Color.white.WithAlpha(0.1f), 0.2f);
                }
                
            }
        }
    }


    public void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
