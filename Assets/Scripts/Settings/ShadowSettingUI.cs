using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSettingUI : VisualSettingUI
{
    public ShadowSetting[] settings;

    public override void Refresh(GraphicSetting graphics)
    {
        dropdown.value = graphics.shadow;
    }

    public override void SetQualitySetting(int quality)
    {
        QualitySettings.shadows = settings[quality].shadows;
        QualitySettings.shadowResolution = settings[quality].shadowResolution;
        QualitySettings.shadowCascades = settings[quality].shadowCascades;
        QualitySettings.shadowDistance = settings[quality].shadowDistance;

        VisualSettingController.GraphicsSetting.shadow = quality;
    }

    [System.Serializable]
    public struct ShadowSetting
    {
        public float shadowDistance;
        public int shadowCascades;
        public ShadowResolution shadowResolution;
        public ShadowQuality shadows;
    }
}
