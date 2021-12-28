using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float comboDuration = 0.5f;

    [SerializeField] Collider2D[] punchAttackCols;
    [SerializeField] Collider2D[] kickAttackCols;

    [SerializeField, Range(0f, 1f)] float punch_StartTiming_1 = 0.45f, punch_StartTiming_2 = 0.45f;
    [SerializeField, Range(0f, 1f)] float kick_StartTiming_1 = 0.45f, kick_StartTiming_2 = 0.45f;

    [SerializeField, Range(0f, 1f)] float punch_EndTiming_1 = 0.45f, punch_EndTiming_2 = 0.45f;
    [SerializeField, Range(0f, 1f)] float kick_EndTiming_1 = 0.45f, kick_EndTiming_2 = 0.45f;

    [SerializeField] int hitEffectCount = 10;

    GameObject[] hitEffects;
    public GameObject[] HitEffects => hitEffects;

    bool isAttacking = false;

    PlayerMove playerMove;
    Animator anim;


    public Coroutine AttackCoroutine { get; private set; }

    public bool IsAttacking => isAttacking;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        InitAttackColliders();

        hitEffects = new GameObject[hitEffectCount];
        hitEffects = HCH.GameObjectPool.GeneratePool(Resources.Load("HitEffect") as GameObject, hitEffectCount);
    }

    void InitAttackColliders()
    {
        for (int i = 0; i < punchAttackCols.Length; i++)
        {
            punchAttackCols[i].enabled = false;
        }
        for (int i = 0; i < kickAttackCols.Length; i++)
        {
            kickAttackCols[i].enabled = false;
        }
    }

    void Update()
    {
        if(InputManager.Instance.GetPunchAttackBtnValue())
        {
            if (!isAttacking)
            {
                playerMove.CanMove = false;
                isAttacking = true;
                if (AttackCoroutine != null) StopCoroutine(AttackCoroutine);
                PunchAttack(anim.GetInteger("PunchAttackPhase"));
                anim.SetInteger("PunchAttackPhase", (anim.GetInteger("PunchAttackPhase") + 1) % 3);
            }
            InputManager.Instance.SetInputValue(out InputManager.Instance.inputButton.punchAttackBtn, false);
        }

        if (InputManager.Instance.GetKickAttackBtnValue())
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
        InputManager.Instance.SetInputValue(out InputManager.Instance.inputButton.kickAttackBtn, false);
    }

    void PunchAttack(int num)
    {
        //print(num);
        switch(num)
        {
            case 0:
            case 1:
                AttackCoroutine = StartCoroutine(PunchAttackCycle_1());
                break;
            case 2:
                AttackCoroutine = StartCoroutine(PunchAttackCycle_2());
                break;
        }
    }

    void KickAttack(int num)
    {
        switch (num)
        {
            case 0:
                AttackCoroutine = StartCoroutine(KickAttackCycle_1());
                break;
            case 1:
                AttackCoroutine = StartCoroutine(KickAttackCycle_2());
                break;
        }
    }

    IEnumerator PunchAttackCycle_1()
    {
        anim.SetTrigger("ToPunchAttack");
        yield return null;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= punch_StartTiming_1);
        punchAttackCols[0].enabled = true;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= punch_EndTiming_1);
        punchAttackCols[0].enabled = false;
        isAttacking = false;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);
        playerMove.CanMove = true;

        yield return new WaitForSeconds(comboDuration);
        anim.SetInteger("PunchAttackPhase", 0);
    }

    IEnumerator PunchAttackCycle_2()
    {
        anim.SetTrigger("ToPunchAttack");
        yield return null;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= punch_StartTiming_2);
        punchAttackCols[1].enabled = true;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= punch_EndTiming_2);
        punchAttackCols[1].enabled = false;
        isAttacking = false;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);
        playerMove.CanMove = true;

        yield return new WaitForSeconds(comboDuration);
        anim.SetInteger("PunchAttackPhase", 0);
    }

    IEnumerator KickAttackCycle_1()
    {
        anim.SetTrigger("ToKickAttack");
        yield return null;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= kick_StartTiming_1);
        kickAttackCols[0].enabled = true;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= kick_EndTiming_1);
        kickAttackCols[0].enabled = false;
        isAttacking = false;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);
        playerMove.CanMove = true;

        yield return new WaitForSeconds(comboDuration);
        anim.SetInteger("KickAttackPhase", 0);
    }

    IEnumerator KickAttackCycle_2()
    {
        anim.SetTrigger("ToKickAttack");
        yield return null;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= kick_StartTiming_2);
        kickAttackCols[1].enabled = true;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= kick_EndTiming_2);
        kickAttackCols[1].enabled = false;
        isAttacking = false;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);
        playerMove.CanMove = true;

        yield return new WaitForSeconds(comboDuration);
        anim.SetInteger("KickAttackPhase", 0);
    }

    public void SetAttackingFalse() => isAttacking = false;
}
