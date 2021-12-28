using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour, IButton
{
    OptionPanel optionPanel;

    OptionPanel OptionPanel => optionPanel = optionPanel ? optionPanel : UIManager.Instance.FindOptionPanel();

    private void Start() => GetComponent<Button>().onClick.AddListener(() => Reaction());

    public void Reaction()
    {
        SoundManager.Instance.PlayEnvironmentOneShot(SoundManager.Instance.ButtonClickSounds, 0.6f);
        OptionPanel.OpenPanel();
    }
}
