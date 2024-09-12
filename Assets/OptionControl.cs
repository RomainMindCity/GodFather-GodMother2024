using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class OptionControl : MonoBehaviour
{
    private string _key;
    [SerializeField] private string _control;
    [SerializeField] private int _bindingNumber;
    [SerializeField] private TextMeshProUGUI _text;
    bool _doChange = false;
    PlayerInput PI;

    void OnEnable()
    {
        PI = FindObjectOfType<PlayerInput>();
        if (PlayerPrefs.HasKey(_control + _bindingNumber))
        {
            _key = PlayerPrefs.GetString($"{_control + _bindingNumber}");
            PI.actions[$"{_control}"].ChangeBinding(_bindingNumber).WithPath($"<Keyboard>/{_key}");
        }
        else
        {
            _key = PI.actions[$"{_control}"].bindings[_bindingNumber].path.Split('/')[1];
        }
        _text.text = PI.actions[$"{_control}"].bindings[_bindingNumber].path.Split('/')[1];
        _text.text = _text.text.ToUpper();
    }

    public void ChangeKey()
    {
        _doChange = true;
    }

    void Update()
    {
        if (_doChange)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                _key = Input.inputString.ToUpper();
                _text.text = _key;
                PI.actions[$"{_control}"].ChangeBinding(_bindingNumber).WithPath($"<Keyboard>/{_key}");
                _doChange = false;
                PlayerPrefs.SetString(_control + _bindingNumber, _key);
                PlayerPrefs.Save();
            }
        }
    }
}
