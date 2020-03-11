using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettingUI : VisualSettingUI
{
    private FullScreenMode[] screens = new FullScreenMode[2] { FullScreenMode.FullScreenWindow, FullScreenMode.Windowed };

    public GraphicSO graphicSO;

    protected override void Awake()
    {
        base.Awake();

        dropdown.options.Clear();
        foreach(FullScreenMode screenMode in screens)
        {
            dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData(screenMode.ToString()));
        }
    }

    public override void Refresh()
    {
        int val = 0;
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].ToString() == Screen.fullScreenMode.ToString())
            {
                val = i;
            }
        }

        dropdown.value = val;
    }

    public override void SetQualitySetting(int quality)
    {
        Screen.fullScreenMode = screens[quality];
        graphicSO.screenMode = screens[quality];
    }

    public FullScreenMode GetCurrentScreenMode()
    {
        return screens[dropdown.value];
    }
}
