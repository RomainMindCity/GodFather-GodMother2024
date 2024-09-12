using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private Animator animator;
    Vector2 _moveInput;
    bool _canWalk;


    const string  idle = "Idle";
    const string front = "Front";
    const string left = "Left";
    const string back = "Back";


    [SerializeField] bool _canBeStopped = false; // Pour le mode endless (oui je fais chier)
    string _currentAnimation;

    public CanvasManager CanvasManager { get => _canvasManager; set => _canvasManager = value; }

    void Start()
    {
        _canvasManager = GameObject.Find("Manager").GetComponent<CanvasManager>();
        //Cursor.lockState = CursorLockMode.Locked;
        _canWalk = true;
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
    }    

    void PlayerMovement()
    {

        if (_canBeStopped && Input.GetMouseButton(0)) { return; }

        Vector2 moveDirection = new Vector2(_moveInput.x, _moveInput.y).normalized;
        gameObject.transform.Translate(moveDirection * _speed * Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Debug.Log(_moveInput);
        switch (_moveInput.x)
        {
            case 1:
                GetComponent<SpriteRenderer>().flipX = false;
                ChangeAnimationState(left);
                break;
            case -1:
                GetComponent<SpriteRenderer>().flipX = true;
                ChangeAnimationState(left);
                break;
            default:
                switch (_moveInput.y)
                {
                    case 1:
                        ChangeAnimationState(back);
                        break;
                    case -1:
                        ChangeAnimationState(front);
                        break;
                    case 0:
                        ChangeAnimationState(idle);
                        break;
                }
                break;
        }
    }

    void ChangeAnimationState(string animationName)
    {
        if (_currentAnimation == animationName) return;

        animator.Play(animationName);
        _currentAnimation = animationName;
    }
}
