using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Variable
{
    [Header(" - Related to Enemy's skill")]
    [SerializeField] internal GameObject skillEffect;
    [SerializeField] internal int maxSkillEffectPoolCount;
    [SerializeField, Range(0f, 1f)] internal float skillEffectTiming;
    [SerializeField] internal float skillWeight = 800;

    internal GameObject[] skillEffects;

    [Header(" - Related to Enemy's attack")]
    [SerializeField] internal Collider2D attackCol;
    [SerializeField] internal float waitToAttack, waitToSkill;
    [SerializeField] internal float startAttackTime = 0.53f, endAttackTime = 0.57f;

    [Header(" - Sound")]
    [SerializeField] internal AudioClip[] attackVoiceClips;
    [SerializeField] internal AudioClip[] skillEffectClips;

    [Space(15)]
    [Tooltip("Max Aggro Duration: After end duration, change to patrol")]
    [SerializeField] internal float aggroDuration = 10;

    internal Coroutine Co_Patrol, Co_Trace, Co_Attack;

    internal EnemyHP enemyHP;
}

public class NormalEnemyFSM : EnemyFSM, IIdle, IPatrol, ITrace, IAttack_1, ISkill_1
{
    #region Variable

    [SerializeField] Variable variable;

    #endregion

    #region Property
    EnemyHP EnemyHP => variable.enemyHP = variable.enemyHP ? variable.enemyHP : GetComponent<EnemyHP>();
    public Coroutine AttackCoroutine => variable.Co_Attack;

    #endregion

    #region Unity Life Cycle

    private new void Start()
    {
        base.Start();
        variable.skillEffects = new GameObject[variable.maxSkillEffectPoolCount];
        variable.skillEffects = HCH.GameObjectPool.GeneratePool(variable.skillEffect, variable.maxSkillEffectPoolCount, FolderSystem.Instance.Bringer_SkillPool, false);
    }

    #endregion

    #region Implementation Place 

    public override IEnumerator Co_Pattern()
    {
        yield return new WaitForSeconds(waitStart);
        while (true)
        {
            yield return variable.Co_Patrol = StartCoroutine(EnemyPatrol());
            yield return variable.Co_Trace = StartCoroutine(EnemyTrace());
        }
    }

    public IEnumerator EnemyIdle()
    {
        //print("나 가만히 있는다!");
        anim.SetTrigger("ToIdle");
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator EnemyPatrol()
    {
        float randomDirTime = 0;
        float randomPatrolTime = Random.Range(3.5f, 5f);
        Vector2 moveVec = RandomVec();

        while (GetDistanceB2WPlayer() > playerDetectRange)
        {
            spriteRenderer.flipX = moveVec == Vector2.right ? true : false;

            if (randomDirTime < randomPatrolTime)
            {
                randomDirTime += Time.deltaTime;
            }
            else
            {
                randomPatrolTime = Random.Range(3.5f, 5f);
                randomDirTime = 0;
                moveVec = RandomVec();
            }

            if (!IsNotWall)
            {
                moveVec *= -1;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            anim.SetBool("IsWalk", moveVec != Vector2.zero);
            transform.Translate(moveVec * moveSpeed * 0.5f * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator EnemyTrace()
    {
        float traceCount = 0;

        while (true)
        {
            FlipCheck();
            if (GetDistanceB2WPlayer() <= attackRange)
            {
                anim.SetBool("IsWalk", false);
                traceCount = 0;
                if (HCH_Random.Random.Genrand_Int32(3) < variable.skillWeight)
                    yield return variable.Co_Attack = StartCoroutine(EnemyAttack_1());
                else
                    yield return variable.Co_Attack = StartCoroutine(EnemySkill_1());

                yield return StartCoroutine(EnemyIdle());
            }
            else if (GetDistanceB2WPlayer() < playerDetectRange)
            {
                traceCount = 0;
                anim.SetBool("IsWalk", true);
                yield return StartCoroutine(Move());
            }
            else
            {
                if (traceCount <= variable.aggroDuration)
                {
                    anim.SetBool("IsWalk", true);
                    yield return StartCoroutine(Move());

                    traceCount += Time.deltaTime;
                }
                else
                {
                    yield break;
                }
            }
        }
    }

    public IEnumerator EnemyAttack_1()
    {
        FlipCheck();
        //print("나 너 때린다!");
        anim.SetTrigger("ToAttack");
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsTag("AttackAnimation"));

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= variable.startAttackTime);
        variable.attackCol.enabled = true;

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= variable.endAttackTime);
        variable.attackCol.enabled = false;

        yield return variable.waitToAttack;
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsTag("IdleAnimation"));
    }

    public IEnumerator EnemySkill_1()
    {
        EnemyHP.SetAbsolute(true);

        FlipCheck();
        //print("이거 아프다!");

        anim.SetTrigger("ToSkill");
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsTag("CastAnimation"));

        anim.SetFloat("AttackSpeed", 0.65f);

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= variable.skillEffectTiming);
        GodHand();

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f);
        anim.SetFloat("AttackSpeed", 0.3f);

        EnemyHP.SetAbsolute(false);

        yield return variable.waitToSkill;
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsTag("IdleAnimation"));
    }

    Vector2 RandomVec() => HCH_Random.Random.Genrand_Int32(1) <= 3 ? Vector2.zero : HCH_Random.Random.Genrand_Int32(1) >= 6 ? Vector2.right : Vector2.left;

    public void SetAttackSpeed() => anim.SetFloat("AttackSpeed", attackSpeed);

    /// <summary> Use Bringer's Skill </summary>
    void GodHand() => HCH.GameObjectPool.PopObjectFromPool(variable.skillEffects, PlayerSystem.Instance.Player.transform.position + Vector3.up * 2.5f);

    #endregion
}
