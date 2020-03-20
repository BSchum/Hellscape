using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualSettingController : MonoBehaviour
{
    public VisualSettingUI[] visualSettings;

    private GraphicSetting graphicSetting;
    public GraphicSetting GraphicsSetting { get { return graphicSetting; } }

    private void Awake()
    {
        graphicSetting = SaveSystem.LoadData<GraphicSetting>(SaveSystem.Data.GraphicSetting);
        if (graphicSetting == null)
        {
            graphicSetting = new GraphicSetting();
        }

        UpdateSettingsUI();
    }

   public void UpdateSettingsUI()
    {
        foreach (VisualSettingUI setting in visualSettings)
        {
            setting.Refresh(graphicSetting);
        }

        SaveSystem.SaveData(graphicSetting, SaveSystem.Data.GraphicSetting);
    }
}
