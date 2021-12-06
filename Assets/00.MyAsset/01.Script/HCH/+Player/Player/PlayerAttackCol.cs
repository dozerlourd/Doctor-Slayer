using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCol : MonoBehaviour
{
    [SerializeField] float damage;

    Collider2D col2D;

    private void Start() => col2D = GetComponent<Collider2D>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            col.GetComponent<HPControllerToEnemy>().TakeDamage(damage);
            col2D.enabled = false;
        }
    }
}
