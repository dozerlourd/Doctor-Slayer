using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingletonObject<UIManager>
{
    public List<GameObject> panelList { get; private set; }
    public List<GameObject> buttonList { get; private set; }

    private void Start()
    {
        StartCoroutine(ActivatePlayerUI());

        panelList = new List<GameObject>();
        buttonList = new List<GameObject>();

        FindChildObjects(transform);

        print(panelList.Count);
        print(buttonList.Count);
    }

    void FindChildObjects(Transform _this)
    {
        if(_this.TryGetComponent(out IPanel _panel))
        {
            panelList.Add(_this.gameObject);
        }
        else if (_this.TryGetComponent(out IButton _button))
        {
            buttonList.Add(_this.gameObject);
        }

        for (int i = 0; i < _this.transform.childCount; i++)
        {
            //print($"{_this.name} : " + _this.childCount);
            FindChildObjects(_this.GetChild(i));
        }
    }

    IEnumerator ActivatePlayerUI()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "PlayScene");

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
