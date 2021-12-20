using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StopPanel : Panel
{


    public override void OpenPanel()
    {
        base.OpenPanel();
        SetTimeScale(0f);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        SetTimeScale(1f);
    }

    void SetTimeScale(float _timeScale)
    {
        Time.timeScale = _timeScale;
    }
}
