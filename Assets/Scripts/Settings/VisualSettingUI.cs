using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class VisualSettingUI : MonoBehaviour
{
    protected Dropdown dropdown;

    protected virtual void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }

    public abstract void SetQualitySetting(int quality);

    public abstract void Refresh();
}
