using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClearPanel : StopPanel
{
    [SerializeField] Button urlButton;

    private void Start()
    {
        urlButton.onClick.AddListener(LoadURLWebSite);
    }

    void LoadURLWebSite()
    {
        Application.OpenURL("https://teamscare.co.kr/");
    }
}
