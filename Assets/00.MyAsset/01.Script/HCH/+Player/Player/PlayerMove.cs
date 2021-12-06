using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Variable

    [SerializeField] float moveSpeed = 200;
    [SerializeField] float jumpForce = 400;

    PlayerAttack playerAttack;

    float moveX;
    bool canMove = true;

    bool falling = false;

    Vector2 frontDir;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    #endregion

    public bool CanMove { get => canMove; set => canMove = value; }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        anim.SetBool("IsMove", moveX != 0 && !playerAttack.IsAttacking && CanMove);
        FlipCheck(moveX);

        if (CanMove)
        {
            if (!RayDerectWall())
            {
                rigid.velocity = new Vector2(moveX * moveSpeed * 100 * Time.deltaTime, rigid.velocity.y);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (rigid.velocity.y == 0 && !anim.GetBool("IsJumping"))
                {
                    anim.SetBool("IsJumping", true);
                    rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (rigid.velocity.y == 0)
        {
            if (anim.GetBool("IsJumping") && falling)
            {
                anim.SetBool("IsJumping", false);
                falling = false;
            }
        }
        if (rigid.velocity.y < -0.01f)
        {
            falling = true;
        }
    }

    void FlipCheck(float hValue)
    {
        frontDir = hValue == 0 ? frontDir : hValue == 1 ? Vector2.right : Vector2.left;
        spriteRenderer.flipX = hValue == 0 ? spriteRenderer.flipX : hValue == 1 ? false : true;
    }

    bool RayDerectWall()
    {
        return Physics2D.Raycast(transform.position, frontDir, moveSpeed * Time.deltaTime, LayerMask.GetMask("L_Ground"));
    }
}
