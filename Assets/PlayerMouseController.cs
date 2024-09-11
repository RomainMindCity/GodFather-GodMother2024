using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMouseController : MonoBehaviour
{
    public float range;
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

        ShotRaycast();
    }

    void ShotRaycast()
    {
        for (int i = 0; i < 70; i += 10)
        {
            Vector3 direction = Quaternion.Euler(0, 0, i - 30) * transform.up;
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, direction, range);
            Debug.DrawRay(transform.position, direction * range, Color.red);
            if (hit2.collider != null)
            {
                Debug.Log(hit2.collider.name);
                if (hit2.collider.CompareTag("Monster"))
                {
                    hit2.collider.GetComponent<Monster>().FlashMonster();
                }
            }
        }
    }

}
