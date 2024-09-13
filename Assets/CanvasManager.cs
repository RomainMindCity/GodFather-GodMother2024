using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsCanvas;

    bool _isInMenu;
    public bool IsInMenu { get => _isInMenu; set => _isInMenu = value; }

    public void DesacSettings()
    {
        _settingsCanvas.SetActive(false);
    }

    public void CloseSettings()
    {
        _mainMenuCanvas.SetActive(true);
        _settingsCanvas.SetActive(false);
        _settingsCanvas.GetComponentInParent<Canvas>().sortingOrder = 0;
        IsInMenu = false;
    }

    public void UpdateCanvas()
    {
        _settingsCanvas.GetComponentInParent<Canvas>().sortingOrder = _settingsCanvas.activeSelf ? 0 : 1;
        _mainMenuCanvas.SetActive(!_mainMenuCanvas.activeSelf);
        _settingsCanvas.SetActive(!_settingsCanvas.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
