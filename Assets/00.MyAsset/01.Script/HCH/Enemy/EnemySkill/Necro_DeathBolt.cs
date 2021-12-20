using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necro_DeathBolt : MonoBehaviour
{
    #region Variable

    [SerializeField] float expRadius = 0.5f;
    [SerializeField] float damage = 7;
    [SerializeField] float gracePariod = 0.2f;

    [Space(15)]
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float rotSpeed = 30;
    [SerializeField] float Duration = 10;

    Vector3 forwardDir;

    [SerializeField] GameObject BloodEffect;

    SpriteRenderer spriteRenderer;

    #endregion

    #region Unity Life Cycle

    void OnEnable()
    {
        StartCoroutine(FlipCheck());
        StartCoroutine(ExpAfterSetTime());
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GuidedMove();
    }

    #endregion

    #region Implementation Place

    #region Explosion Related Method

    IEnumerator FlipCheck()
    {
        yield return new WaitForEndOfFrame();
        forwardDir = spriteRenderer.flipX ? -transform.right : transform.right;
    }

    IEnumerator ExpAfterSetTime()
    {
        yield return new WaitForSeconds(Duration);
        VanishedAndShowEffect();
    }

    void VanishedAndShowEffect()
    {
        Instantiate(BloodEffect, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
        StopAllCoroutines();
    }

    void Explosion(PlayerHP _playerHP)
    {
        _playerHP.TakeDamage(damage);
        gameObject.SetActive(false);
    }

    #endregion

    #region Movement Related Method

    void GuidedMove()
    {

        Vector3 targetPos = PlayerSystem.Instance.Player.transform.position;
        Vector3 targerDir = (targetPos - transform.position).normalized;

        float angle = Mathf.Atan2(targerDir.y, targerDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotSpeed * Time.deltaTime);
        spriteRenderer.flipY = Mathf.Abs(transform.eulerAngles.z) >= 90 && Mathf.Abs(transform.eulerAngles.z) <= 270;

        forwardDir.y = 0;
        transform.Translate(forwardDir * moveSpeed * Time.deltaTime);
    }

    #endregion

    #region Setter

    /// <summary> For Setting Death Bolt's Flip Value </summary>
    /// <param name="val"> Death Bolt's Flip Value </param>
    public void SetFlip(bool val) => spriteRenderer.flipX = val;

    #endregion

    #region Callback Method

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Instantiate(BloodEffect, transform.position, Quaternion.identity);
            Explosion(col.GetComponent<PlayerHP>());
        }
    }

    #endregion

    #endregion
}
