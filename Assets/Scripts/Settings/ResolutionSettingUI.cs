using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSettingUI : VisualSettingUI
{
    public override void Refresh(GraphicSetting graphics)
    {
        int val = 0;
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (i == graphics.resolution)
            {
                val = i;
            }
        }

        dropdown.value = val;
    }

    public override void SetQualitySetting(int quality)
    {
        string resolution = dropdown.options[dropdown.value].text;

        string h = resolution.Split(':')[0];
        string w = resolution.Split(':')[1];

        VisualSettingController.GraphicsSetting.resolution = quality;

        Screen.SetResolution(int.Parse(h), int.Parse(w), VisualSettingController.GraphicsSetting.isFullScreen);
    }
}
