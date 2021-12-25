using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour, IButton
{
    Button optionBtn;
    Button OptionBtn => optionBtn = optionBtn ? optionBtn : GetComponent<Button>();

    OptionPanel optionPanel;

    OptionPanel OptionPanel => optionPanel = optionPanel ? optionPanel : FindOptionPanel();

    void Start()
    {
        OptionBtn.onClick.AddListener(() => Reaction());
    }

    public void Reaction()
    {
        SoundManager.Instance.PlayEnvironmentOneShot(SoundManager.Instance.ButtonClickSounds, 0.6f);
        OptionPanel.OpenPanel();
    }

    OptionPanel FindOptionPanel()
    {
        for (int i = 0; i < UIManager.Instance.panelList.Count; i++)
        {
            if (UIManager.Instance.panelList[i].TryGetComponent(out OptionPanel op))
            {
                return op;
            }
        }
        return null;
    }
}
