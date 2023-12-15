using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaOptions : MonoBehaviour
{
    public Slider Slider_SFX, Slider_BGM;

    private void OnEnable()
    {
        Slider_SFX.value = AudioManager.instance.source_SFX.volume;
        Slider_BGM.value = AudioManager.instance.source_BGM.volume;
    }

    public void ChangeVolume(bool sfx)
    {
        if (sfx)
        {
            AudioManager.instance.source_SFX.volume = Slider_SFX.value;
        }
        else
        {
            AudioManager.instance.source_BGM.volume = Slider_BGM.value;
        }
    }
}
