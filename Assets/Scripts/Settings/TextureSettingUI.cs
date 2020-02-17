using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSettingUI : VisualSettingUI
{
    public override void Refresh()
    {
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    public override void SetQualitySetting(int quality)
    {
        QualitySettings.pixelLightCount = quality;
    }
}
