using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    #region Variable
    SpriteRenderer spriteRenderer;
    #endregion

    #region Property
    SpriteRenderer SpriteRenderer => spriteRenderer = spriteRenderer ? spriteRenderer : transform.parent.GetComponent<SpriteRenderer>();
    #endregion

    private void Update()
    {
        transform.eulerAngles = new Vector3(0, SpriteRenderer.flipX ? 180 : 0, 0);
    }
}
