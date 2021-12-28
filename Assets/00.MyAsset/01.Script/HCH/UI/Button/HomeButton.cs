using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour, IButton
{
    private void Start() => GetComponent<Button>().onClick.AddListener(() => Reaction());

    public void Reaction()
    {
        SoundManager.Instance.PlayEnvironmentOneShot(SoundManager.Instance.ButtonClickSounds, 0.6f);
        UIManager.Instance.FindOptionPanel().ClosePanel();
        SceneManager.LoadScene("LobbyScene");
    }
}
