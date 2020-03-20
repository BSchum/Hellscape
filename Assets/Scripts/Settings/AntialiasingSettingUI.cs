using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntialiasingSettingUI : VisualSettingUI
{
    int[] antialiasingSettings = new int[] { 0, 2, 4, 8 };

    private void Start()
    {
        dropdown.options.Clear();

        foreach (int antialiasing in antialiasingSettings)
        {
            dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData(antialiasing + " Multi Sampling"));
        }

        dropdown.options[0].text = "Disabled";
    }

    public override void Refresh(GraphicSetting graphics)
    {
        dropdown.value = graphics.antiAliasing;
    }

    public override void SetQualitySetting(int quality)
    {
        QualitySettings.antiAliasing = antialiasingSettings[quality];
        VisualSettingController.GraphicsSetting.antiAliasing = quality;
    }
}
