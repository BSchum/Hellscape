using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettingUI : VisualSettingUI
{
    private FullScreenMode[] screens = new FullScreenMode[2] { FullScreenMode.FullScreenWindow, FullScreenMode.Windowed };

    protected override void Awake()
    {
        base.Awake();

        dropdown.options.Clear();
        foreach(FullScreenMode screenMode in screens)
        {
            dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData(screenMode.ToString()));
        }
    }

    public override void Refresh(GraphicSetting graphics)
    {
        int val = 0;
        for (int i = 0; i < screens.Length; i++)
        {
            if (i == graphics.screenMode)
            {
                val = i;
            }
        }
        dropdown.value = val;
    }

    public override void SetQualitySetting(int quality)
    {
        Screen.fullScreenMode = screens[quality];
        VisualSettingController.GraphicsSetting.screenMode = quality;
    }

    public FullScreenMode GetCurrentScreenMode()
    {
        return screens[dropdown.value];
    }
}
