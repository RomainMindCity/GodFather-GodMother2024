using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CanvasManager _canvasManager;
    Vector2 _moveInput;

    [SerializeField] bool _canBeStopped = false; // Pour le mode endless (oui je fais chier)

    public CanvasManager CanvasManager { get => _canvasManager; set => _canvasManager = value; }

    void Start()
    {
        _canvasManager = GameObject.Find("Manager").GetComponent<CanvasManager>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Cursor.lockState = _canvasManager.IsInMenu ? CursorLockMode.None : CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            _canvasManager.IsInMenu = !_canvasManager.IsInMenu;
            _canvasManager.UpdateCanvas();
        }
        if (!_canvasManager.IsInMenu) PlayerMovement();
        PlayerCollision();
    }    

    void PlayerMovement()
    {

        if (_canBeStopped && Input.GetMouseButton(0)) { return; }

        Vector2 moveDirection = new Vector2(_moveInput.x, _moveInput.y).normalized;
        gameObject.transform.Translate(moveDirection * _speed * Time.deltaTime);
    }

    void PlayerCollision()
    {
        //faire la logique de collision ici
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
}
