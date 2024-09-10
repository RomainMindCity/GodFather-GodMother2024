using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMouseController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    float xRotation;
    void Update()
    {
        if (gameObject.GetComponentInParent<PlayerController>().CanvasManager.IsInMenu) return;
        
        Vector3 mousePosition = Input.mousePosition;
        
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

    }

}
