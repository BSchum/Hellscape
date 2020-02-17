using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSettingUI : VisualSettingUI
{
    public override void Refresh() { }

    public override void SetQualitySetting(int quality)
    {
        string resolution = dropdown.options[dropdown.value].text;
        string h = resolution.Split(':')[0];
        string w = resolution.Split(':')[1];

        Screen.SetResolution(int.Parse(h), int.Parse(w), ScreenSettingUI.currentScreenMode);
    }
}
