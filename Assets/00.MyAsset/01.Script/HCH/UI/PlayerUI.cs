using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image playerHPBar;

    private void Start()
    {
        StartCoroutine(SearchingPlayer());
    }

    IEnumerator SearchingPlayer()
    {
        yield return new WaitUntil(() => PlayerSystem.Instance.Player);
        PlayerSystem.Instance.Player.GetComponent<PlayerHP>().SetHpBar(ref playerHPBar);
    }
}
