using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSettingUI : VisualSettingUI
{
    public GraphicSO graphicSO;

    public override void Refresh() 
    {
        int val = 0;
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text == graphicSO.resolution)
            {
                val = i;
            }
        }

        dropdown.value = val;
    }

    public override void SetQualitySetting(int quality)
    {
        string resolution = dropdown.options[dropdown.value].text;

        graphicSO.resolution = resolution;
        
        string h = resolution.Split(':')[0];
        string w = resolution.Split(':')[1];

        Screen.SetResolution(int.Parse(h), int.Parse(w), graphicSO.screenMode);
    }
}
