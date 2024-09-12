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
    private Coroutine flashCoroutine;

    [SerializeField] private bool _clickToActivate = true;

    void Update()
    {
        if (gameObject.GetComponentInParent<PlayerController>().CanvasManager.IsInMenu) return;

        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        if (_clickToActivate && !Input.GetMouseButton(0))
        {
            transform.Find("Point Light 2D").gameObject.SetActive(false);
            return;
        } else if (_clickToActivate && Input.GetMouseButton(0))
        {
            transform.Find("Point Light 2D").gameObject.SetActive(true);
        }



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
                    switch (hit.collider.tag)
                    {
                        case "Monster":
                            if (hit.collider.gameObject.transform.parent.name == "Bracken") 
                            {
                                StartCoroutine(FlashMonster(hit.collider.gameObject));
                            }

                            if (hit.collider.gameObject.transform.parent.name == "CoilHead") 
                            {
                                flashCoroutine = StartCoroutine(FlashMonster(hit.collider.gameObject));
                                if (coroutine != null)
                                {
                                    StopCoroutine(coroutine);
                                }
                                coroutine = StartCoroutine(UnflashMonster(hit.collider.gameObject));
                            }
                            break;

                        default:
                            break;
                    }
                }
                
            }
        }
    }

    public Vector2 GetPlayerPosition()
    {
        return transform.position;
    }

    IEnumerator FlashMonster(GameObject monster)
    {
        yield return new WaitForSeconds(0.1f);
        monster.GetComponentInParent<MonsterBehavior>().FlashMonster();
    }

    IEnumerator UnflashMonster(GameObject monster)
    {
        yield return new WaitForSeconds(1);
        monster.GetComponentInParent<CoilHead>().UnflashMonster();
        StopCoroutine(flashCoroutine);
    }

}
