using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSettingUI : VisualSettingUI
{
    public override void Refresh(GraphicSetting graphics)
    {
        dropdown.value = graphics.texture;
    }

    public override void SetQualitySetting(int quality)
    {
        QualitySettings.pixelLightCount = quality;
        VisualSettingController.GraphicsSetting.texture = quality;
    }
}
