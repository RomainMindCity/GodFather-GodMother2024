using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsCanvas;

    bool _isInMenu;
    public bool IsInMenu { get => _isInMenu; set => _isInMenu = value; }

    public void CloseSettings()
    {
        _mainMenuCanvas.SetActive(true);
        _settingsCanvas.SetActive(false);
        IsInMenu = false;
    }

    public void UpdateCanvas()
    {
        _mainMenuCanvas.SetActive(!_mainMenuCanvas.activeSelf);
        _settingsCanvas.SetActive(!_settingsCanvas.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
