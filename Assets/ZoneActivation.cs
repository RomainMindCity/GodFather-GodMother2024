using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneActivation : MonoBehaviour
{
    [Header("List Monsters - Pour utiliser : Mettre les monstres que vous voulez activer, et edit le collider pour la zone")]
    [Tooltip("Liste des monstres à mettre absolument dedans, si pas dedans, ils ne sont pas désactivés au départ \n Il faut aussi faire la zone avec le edit collider")]
    [SerializeField] List<GameObject> _monsters = new List<GameObject>();

    void Start()
    {
        foreach (GameObject monster in _monsters)
        {
            monster.GetComponent<MonsterBehavior>().ChangeStateAI(MonsterBehavior.StatesBehavior.NONE);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

       if (collision.gameObject.tag == "Player")
        {
            foreach (GameObject monster in _monsters)
            {
                monster.GetComponent<MonsterBehavior>().ChangeStateAI(MonsterBehavior.StatesBehavior.AI);
            }
        }
    }
}
