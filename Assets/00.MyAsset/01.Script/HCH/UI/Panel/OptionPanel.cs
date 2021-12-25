using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : StopPanel
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider effectSlider;

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(delegate { SoundManager.Instance.SetBGMVolume(bgmSlider.value); });
        effectSlider.onValueChanged.AddListener(delegate { SoundManager.Instance.SetEffectVolume(effectSlider.value); });
    }
}
