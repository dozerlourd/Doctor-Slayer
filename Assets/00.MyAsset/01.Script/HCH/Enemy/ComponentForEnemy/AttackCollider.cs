using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] float attackDamage;

    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            //print(col.gameObject.name);
            col.GetComponent<PlayerHP>()?.TakeDamage(attackDamage);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
