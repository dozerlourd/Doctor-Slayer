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
        while(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LobbyScene")
        {
            if (!SceneEffectSystem.Instance) continue;
            gameObject.SetActive(!SceneEffectSystem.Instance.GetBGBActivation());
            yield return null;
        }
    }
}
