using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingletonObject<UIManager>
{
    public List<GameObject> PanelList { get; private set; }
    public List<GameObject> ButtonList { get; private set; }

    public GameObject BossCanvas { get; private set; }

    private void Start()
    {
        StartCoroutine(ActivatePlaySceneUI());

        PanelList = new List<GameObject>();
        ButtonList = new List<GameObject>();

        FindChildObjects(transform);
    }

    void FindChildObjects(Transform _this)
    {
        if(_this.TryGetComponent(out IPanel _panel))
        {
            PanelList.Add(_this.gameObject);
        }
        else if (_this.TryGetComponent(out IButton _button))
        {
            ButtonList.Add(_this.gameObject);
        }

        for (int i = 0; i < _this.transform.childCount; i++)
        {
            //print($"{_this.name} : " + _this.childCount);
            FindChildObjects(_this.GetChild(i));
        }
    }

    IEnumerator ActivatePlaySceneUI()
    {
        string currSceneName = SceneManager.GetActiveScene().name;

        ActivatePlayerUI();
        DisActivateBossUI();

        while (true)
        {
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name != currSceneName);
            currSceneName = SceneManager.GetActiveScene().name;
            ActivatePlayerUI();
        }
    }

    void ActivatePlayerUI() => transform.GetChild(0).gameObject.SetActive(SceneManager.GetActiveScene().name == "PlayScene");

    void DisActivateBossUI()
    {

        BossCanvas = transform.GetChild(1).gameObject;
        BossCanvas.SetActive(false);
    }

    public OptionPanel FindOptionPanel()
    {
        for (int i = 0; i < PanelList.Count; i++)
        {
            if (PanelList[i].TryGetComponent(out OptionPanel op))
            {
                return op;
            }
        }
        return null;
    }
}
