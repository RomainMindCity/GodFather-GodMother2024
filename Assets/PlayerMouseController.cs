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
    private Coroutine coroutine;
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
        for (int i = 0; i < 50; i += 10)
        {
            Vector3 direction = Quaternion.Euler(0, 0, i - 20) * transform.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range);
            Debug.DrawRay(transform.position, direction * range, Color.red);
            if (hit.collider != null)
            {
                if (hit.collider.tag != "Wall")
                {
                    Debug.Log(hit.collider.tag);
                    switch (hit.collider.tag)
                    {
                        case "Bracken":
                            hit.collider.GetComponentInParent<Bracken>().FlashMonster();
                            break;
                        case "CoilHead":
                            hit.collider.GetComponentInParent<CoilHead>().FlashMonster();
                            if (coroutine != null)
                            {
                                StopCoroutine(coroutine);
                            }
                            coroutine = StartCoroutine(UnflashMonster(hit.collider.gameObject));
                            break;
                        default:
                            break;
                    }
                }
                
            }
        }
    }

    IEnumerator UnflashMonster(GameObject monster)
    {
        yield return new WaitForSeconds(1);
        monster.GetComponentInParent<CoilHead>().UnflashMonster();
    }

}
