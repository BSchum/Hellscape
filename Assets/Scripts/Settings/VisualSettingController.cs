using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualSettingController : MonoBehaviour
{
    public Dropdown graphics;
    public VisualSettingUI[] visualSettings;

    public GraphicSO graphicSO;

    private List<Dropdown.OptionData> unityGraphicOptions = new List<Dropdown.OptionData>();

    private void Awake()
    {
        foreach (string name in QualitySettings.names)
        {
            unityGraphicOptions.Add(new Dropdown.OptionData(name));
        }

        graphics.options = unityGraphicOptions;
    }

    private void OnEnable()
    {
        graphics.value = graphicSO.graphicLevel;
    }

    public void OnChangeGraphicOption()
    {
        QualitySettings.SetQualityLevel(graphics.value, true);
        graphicSO.graphicLevel = QualitySettings.GetQualityLevel();

        UpdateSettingsUI();
    }

    private void UpdateSettingsUI()
    {
        foreach (VisualSettingUI setting in visualSettings)
        {
            setting.Refresh();
        }
    }
}
