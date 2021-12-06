using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCH
{
    public class PlayerMove_NotUse : MonoBehaviour
    {
        #region Variable

        #region SerializedField

        [Header(" - Attack")]
        #region Damage
        [SerializeField] protected float basicAttackDmg = 10;
        #endregion

        [Header(" - Speed")]
        #region Speed
        [SerializeField] protected float moveSpeed = 1.0f;
        [SerializeField] float jumpPower;
        #endregion

        [Header(" - Check Distance")]
        #region Distance Variables
        [SerializeField] protected float groundCheckRayDist = 0.03f;
        #endregion

        [Header(" - Gravity")]
        #region Gravity
        //[SerializeField] protected float gravityScale = 1.00f;
        #endregion

        #endregion

        #region HideInInspector

        Vector2 frontDir;

        BoxCollider2D boxCol2D;
        Animator anim;
        SpriteRenderer spriteRenderer;
        Rigidbody2D rigid2D;

        #endregion

        #endregion

        #region Unity Life Cycle

        void Awake()
        {
            boxCol2D = GetComponent<BoxCollider2D>();
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigid2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Vector2 moveVec = Vector2.zero;

            float h = Input.GetAxisRaw("Horizontal");
            anim.SetBool("IsMove", isGround && h != 0);

            FlipCheck(h);
            if (!RayDerectWall())
            {
                moveVec.x = h;
            }

            if(isGround && !isJumping)
            {
                rigid2D.gravityScale = 0;
                rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0);
            }
            else
            {
                rigid2D.gravityScale = 1;
            }

            transform.Translate(new Vector2(moveVec.x * moveSpeed, rigid2D.velocity.y) * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            GroundCheck(groundCheckRayDist);
            Jump();
        }

        #endregion

        #region Implementation Place 

        #region DetectWall

        bool RayDerectWall()
        {
            return Physics2D.Raycast(transform.position, frontDir, moveSpeed * Time.deltaTime, LayerMask.GetMask("L_Ground"));
        }

        #endregion

        #region Jump

        public bool isJumping = false;

        void Jump()
        {
            if (isJumping)
            {
                if (isGround)
                {
                    isJumping = false;
                }
                else return;
            }

            if (Input.GetButton("Jump"))
            {
                print("GetButtonDown");
                isJumping = true;
                if (rigid2D.velocity.y == 0)
                {
                    rigid2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                }
            }
            #region
            //SoundManager.Instance.PlayEffectOneShot(jumpSounds, 0.2f);

            //Vector2 jumpVelocity = new Vector2(0, jumpPower);

            ////점프 가능 횟수가 0 이상이면
            //if (Input.GetButtonDown("Jump"))
            //{
            //    print("HIAhids");
            //    if (!isJumping)
            //    {
            //        rigid2D.velocity = Vector2.zero;

            //        anim.SetTrigger("Jump");

            //        rigid2D.AddForce(jumpVelocity, ForceMode2D.Impulse);
            //        isJumping = true;
            //    }

            //    isJumping = false;
            //}
            #endregion
        }

        #endregion

        #region GroundCheck

        private bool isGround;

        void GroundCheck(float dist)
        {
            Debug.DrawRay(transform.position, -transform.up * (boxCol2D.size.y / 2), Color.red);
            isGround = Physics2D.Raycast(transform.position, -transform.up, boxCol2D.size.y / 2, LayerMask.GetMask("L_Ground")) ? true : false;
        }

        #endregion

        #region Flip Check

        void FlipCheck(float hValue)
        {
            frontDir = hValue == 0 ? frontDir : hValue == 1 ? Vector2.right : Vector2.left;
            spriteRenderer.flipX = hValue == 0 ? spriteRenderer.flipX : hValue == 1 ? false : true;
        }

        #endregion

        #endregion
    }
}

