using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCol : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] Transform hitPos;

    PlayerAttack playerAttack;
    PlayerAttack PlayerAttack => playerAttack = playerAttack ? playerAttack : PlayerSystem.Instance.Player.GetComponent<PlayerAttack>();

    Collider2D col2D;

    private void Start() => col2D = GetComponent<Collider2D>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            HCH.GameObjectPool.PopObjectFromPool(PlayerAttack.HitEffects, hitPos.position);
            col.GetComponent<HPControllerToEnemy>().TakeDamage(damage);
            col2D.enabled = false;
        }
    }
}
