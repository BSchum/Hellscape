using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettingUI : VisualSettingUI
{
    private FullScreenMode[] screens = new FullScreenMode[2] { FullScreenMode.FullScreenWindow, FullScreenMode.Windowed };
    public static FullScreenMode currentScreenMode;

    private void Start()
    {
        dropdown.options.Clear();
        foreach(FullScreenMode screenMode in screens)
        {
            dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData(screenMode.ToString()));
        }
    }

    public override void Refresh()
    {
        dropdown.value = Screen.fullScreen ? 0 : 1;
    }

    public override void SetQualitySetting(int quality)
    {
        Screen.fullScreenMode = screens[quality];
        currentScreenMode = screens[quality];
    }

    public FullScreenMode GetCurrentScreenMode()
    {
        return screens[dropdown.value];
    }
}
