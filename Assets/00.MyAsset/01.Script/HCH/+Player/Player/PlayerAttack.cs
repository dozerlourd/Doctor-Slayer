using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
internal class AttackCategory
{
    public enum ATTACK_KIND
    {
        PunchAttack1,
        PunchAttack2,
        KickAttack1,
        KickAttack2,
        ShootAttack
    }

    [SerializeField] internal ATTACK_KIND attackKind;

    [SerializeField] internal string animTriggerName;
    [SerializeField] internal string animPhaseIntegerName;
    [SerializeField] internal Collider2D attackCol;
    [SerializeField, Range(0f, 1f)] internal float startColTiming;
    [SerializeField, Range(0f, 1f)] internal float endColTiming;
    [SerializeField, Range(0f, 1f)] internal float endAttackTiming;
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] AttackCategory[] attackCategories;


    [SerializeField] float comboDuration = 0.5f;

    [SerializeField] Collider2D[] punchAttackCols;
    [SerializeField] Collider2D[] kickAttackCols;

    bool isAttacking = false;

    PlayerMove playerMove;
    Animator anim;

    public Coroutine AttackCoroutine { get; private set; }

    public bool IsAttacking => isAttacking;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // 공격을 할 때 PlayerMove의 SetIsMove(value) 값을 false로 변경한다.
        // 공격이 끝난 후 value 값을 true로 변경한다.
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (!isAttacking)
            {
                playerMove.CanMove = false;
                isAttacking = true;
                if (AttackCoroutine != null) StopCoroutine(AttackCoroutine);
                PunchAttack(anim.GetInteger("PunchAttackPhase"));
                anim.SetInteger("PunchAttackPhase", (anim.GetInteger("PunchAttackPhase") + 1) % 3);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isAttacking)
            {
                playerMove.CanMove = false;
                isAttacking = true;
                if (AttackCoroutine != null) StopCoroutine(AttackCoroutine);
                KickAttack(anim.GetInteger("KickAttackPhase"));
                anim.SetInteger("KickAttackPhase", (anim.GetInteger("KickAttackPhase") + 1) % 2);
            }
        }
    }

    void PunchAttack(int num)
    {
        switch(num)
        {
            case 0:
            case 1:
                AttackCoroutine = StartCoroutine(AttackCycle(attackCategories[0]));
                break;
            case 2:
                AttackCoroutine = StartCoroutine(AttackCycle(attackCategories[1]));
                break;
        }
    }

    void KickAttack(int num)
    {
        switch (num)
        {
            case 0:
            case 1:
                AttackCoroutine = StartCoroutine(AttackCycle(attackCategories[2]));
                break;
            case 2:
                AttackCoroutine = StartCoroutine(AttackCycle(attackCategories[3]));
                break;
        }
    }

    IEnumerator AttackCycle(AttackCategory attackCategory)
    {
        anim.SetTrigger(attackCategory.animTriggerName);
        yield return null;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackCategory.startColTiming);
        attackCategory.attackCol.enabled = true;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackCategory.endColTiming);
        attackCategory.attackCol.enabled = false;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= (attackCategory.endAttackTiming == 0 ? 0.9f : attackCategory.endAttackTiming));
        playerMove.CanMove = true;
        isAttacking = false;

        yield return new WaitForSeconds(comboDuration);
        anim.SetInteger(attackCategory.animPhaseIntegerName, 0);
    }

    //public AttackCategory ChoiceAttackCategory(AttackCategory.ATTACK_KIND attack_kind)
    //{
    //    AttackCategory attackCategory;
    //    for (int i = 0; i < attackCategories.Length; i++)
    //    {
    //        attackCategory = attack_kind == attackCategories[i].attackKind ? attackCategories[i] : null;
    //    }
    //    return attackCategory;
    //}
}
