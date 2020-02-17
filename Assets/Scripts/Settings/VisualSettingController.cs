using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualSettingController : MonoBehaviour
{
    public Dropdown graphics;
    public VisualSettingUI[] visualSettings;

    private List<Dropdown.OptionData> unityGraphicOptions = new List<Dropdown.OptionData>();

    private void Start()
    {
        foreach (string name in QualitySettings.names)
        {
            unityGraphicOptions.Add(new Dropdown.OptionData(name));
        }

        graphics.options = unityGraphicOptions;
    }

    private void OnEnable()
    {
        graphics.value = QualitySettings.GetQualityLevel();
    }

    public void OnChangeGraphicOption()
    {
        QualitySettings.SetQualityLevel(graphics.value, true);

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
