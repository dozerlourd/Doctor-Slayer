using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMonsterInfo : MonoBehaviour
{
    [SerializeField] ushort monsterIndex;
    GameObject nameCanvas;

    private void Start()
    {
        nameCanvas = SearchNameCanvas();
        SetStringInfo(nameCanvas.transform);
    }

    GameObject SearchNameCanvas() => transform.Find("Enemy_NameCanvas").gameObject;

    void SetStringInfo(Transform _canvas)
    {
        Text title = _canvas.GetChild(0).GetComponent<Text>();
        Text name = _canvas.GetChild(1).GetComponent<Text>();

        title.text = GlobalState.monsterInfoList[monsterIndex].MonsterTitle;
        name.text = GlobalState.monsterInfoList[monsterIndex].MonsterName;
    }

    public void SetActiveUI()
    {
        nameCanvas.SetActive(false);
    }
}
