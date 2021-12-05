using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCH
{
    public class PlayerMove : MonoBehaviour
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
        [SerializeField] protected float gravityScale = 1.00f;
        #endregion

        #endregion

        #region HideInInspector

        bool canJump = true;

        bool isGround = false;

        Vector2 frontDir;

        BoxCollider2D boxCol2D;
        Animator anim;
        SpriteRenderer spriteRenderer;

        Coroutine Co_Gravity;

        #endregion

        #endregion

        #region Unity Life Cycle

        void Awake()
        {
            boxCol2D = GetComponent<BoxCollider2D>();
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Vector2 moveVec = Vector2.zero;
            JumpCheck(ref moveVec);
            Move(ref moveVec);
            transform.Translate(moveVec * moveSpeed * Time.deltaTime);
        }

        #endregion

        #region Implementation Place 

        #region Movement

        void Move(ref Vector2 Dir)
        {
            float h = Input.GetAxisRaw("Horizontal");

            FlipCheck(h);
            if (!RayDerectWall())
            {
                Dir.x = h;
            }

        }

        bool RayDerectWall()
        {
            Debug.DrawRay(transform.position, frontDir * moveSpeed * Time.deltaTime, Color.red);
            return Physics2D.Raycast(transform.position, frontDir, moveSpeed * Time.deltaTime, LayerMask.GetMask("L_Ground"));
        }

        #endregion

        #region Jump

        void JumpCheck(ref Vector2 Dir)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (canJump)
                {
                    canJump = false;
                    Dir.y = jumpPower;
                }
            }
            Grivaty(ref Dir);
            canJump = true;
        }

        #endregion

        #region Gravity

        void Grivaty(ref Vector2 Dir)
        {
            GroundCheck(groundCheckRayDist);

            if (!isGround)
            {
                Dir.y += -(0.98f * gravityScale);
            }
            else
            {
                Dir.y = 0;
            }
        }

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
