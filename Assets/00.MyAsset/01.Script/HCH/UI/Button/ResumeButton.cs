using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour, IButton
{
    Button resumeBtn;
    Button ResumeBtn => resumeBtn = resumeBtn ? resumeBtn : GetComponent<Button>();

    void Start()
    {
        ResumeBtn.onClick.AddListener(() => Reaction());
    }

    public void Reaction()
    {
        SoundManager.Instance.PlayEnvironmentOneShot(SoundManager.Instance.ButtonClickSounds, 0.6f);
        transform.parent.GetComponent<PanelClass>().ClosePanel();
    }
}
