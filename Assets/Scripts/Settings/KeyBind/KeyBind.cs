using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBind : MonoBehaviour
{
    public KeyBindData.Key key;

    public void OnClick()
    {
        KeybindSetting.SetKey(this);
    }
}
