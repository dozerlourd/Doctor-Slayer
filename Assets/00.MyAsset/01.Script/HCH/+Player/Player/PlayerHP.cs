using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    #region Variable

    [SerializeField] float maxHP;

    bool isDead = false;

    float currHP;

    PlayerAttack playerAttack;
    PlayerMove playerMove;
    Animator anim;

    Coroutine Co_Dead;

    #endregion

    #region Property
    public float MaxHP => maxHP;

    public float CurrHP
    {
        get => currHP;
        set
        {
            if (!isDead)
            {
                currHP = value < 0 ? 0 : value;
                RefreshUI();

                if (NormalizedCurrHP <= 0)
                {
                    Animator.SetTrigger("ToDie");
                    Co_Dead = StartCoroutine(PlayerDead());
                }
            }
        }
    }

    public float NormalizedCurrHP => CurrHP / MaxHP;
    Animator Animator => anim = anim ? anim : GetComponent<Animator>();
    PlayerAttack PlayerAttack => playerAttack = playerAttack ? playerAttack : GetComponent<PlayerAttack>();
    PlayerMove PlayerMove => playerMove = playerMove ? playerMove : GetComponent<PlayerMove>();

    public bool IsDead => isDead;

    #endregion

    #region Unity Life Cycle

    void Awake()
    {
        CurrHP = MaxHP;
    }

    void OnEnable()
    {
        CurrHP = MaxHP;
        GetComponent<Collider2D>().enabled = true;
        isDead = false;
    }

    #endregion

    #region Implementation Place

    public void TakeDamage(float _damage)
    {
        if (isDead) return;

        StartCoroutine(PlayerDamaged());
        DamageUISystem.Instance.DisplayDamageText(_damage, transform);
        CurrHP -= _damage;
    }

    void RefreshUI()
    {
        
    }

    IEnumerator DamagedMove(float damagedTime, Vector2 damagedDir)
    {
        float currentTime = 0;

        while (currentTime <= damagedTime)
        {
            currentTime += Time.deltaTime;

            transform.position += new Vector3(damagedDir.x, damagedDir.y, 0) * Time.deltaTime;

            yield return null;
        }
        PlayerMove.CanMove = true;
    }

    /// <summary> When Enemy Taking Damage, Generate this method </summary>
    IEnumerator PlayerDamaged()
    {
        if (PlayerAttack.AttackCoroutine != null)
        {
            PlayerAttack.StopCoroutine(PlayerAttack.AttackCoroutine);
            PlayerAttack.SetAttackingFalse();
        }
        PlayerMove.CanMove = false;
        Animator.SetTrigger("ToDamaged");
        StartCoroutine(DamagedMove(0.15f, _ = GetComponent<SpriteRenderer>().flipX ? Vector2.right * 2 + Vector2.up * 0.5f : Vector2.left * 2 + Vector2.up * 0.5f));
        yield return null;
    }

    IEnumerator PlayerDead()
    {
        isDead = true;
        Animator.SetTrigger("ToDie");
        yield return null;
    }

    #endregion
}

