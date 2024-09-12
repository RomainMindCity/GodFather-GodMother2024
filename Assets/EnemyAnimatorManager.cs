using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string _currentAnimation;
    const string front = "Front";
    const string back = "Back";
    const string left = "Left";
    const string right = "Right";
    Vector2 oldPosition;
    Vector2 newPosition;

    void Start()
    {
        ChangeAnimationState(front);
    }

    void ChangeAnimationState(string animationName)
    {
        Debug.Log(animationName);
        if (_currentAnimation == animationName) return;

        animator.Play(animationName);
        _currentAnimation = animationName;
    }

}
