using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoActivate : MonoBehaviour
{

    private void Start()
    {
        CoroutineManager.Instance.StartCoroutine(ActivateThis());
    }

    IEnumerator ActivateThis()
    {
        while(true)
        {
            gameObject.SetActive(!SceneEffectSystem.Instance.GetBGBActivation());
            yield return null;
        }
    }
}
