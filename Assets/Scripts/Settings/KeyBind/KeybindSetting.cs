using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindSetting : MonoBehaviour
{
    public KeyBindData binds;
    public Text attack;
    public Text interact;
    public Text left;
    public Text right;
    public Text forward;
    public Text backward;

    private bool isReadingKeys = false;
    private KeyBind keyEditing;

    private static KeybindSetting singleton;

    void Start()
    {
        singleton = this;
        binds = SaveSystem.LoadData<KeyBindData>(SaveSystem.Data.Inputs);
        if (binds == null)
        {
            binds = new KeyBindData();
            SaveSystem.SaveData(binds, SaveSystem.Data.Inputs);
        }

        UpdateBindsUI();
    }

    void Update()
    {
        EditKey();
    }

    void EditKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            isReadingKeys = false;
        }

        if (!isReadingKeys)
            return;

        foreach (KeyCode key in KeyCode.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key) && key != KeyCode.Escape)
            {
                isReadingKeys = false;
                binds.SetKey(keyEditing.key, key);
                SaveSystem.SaveData(binds, SaveSystem.Data.Inputs);
                UpdateBindsUI();
            }
        }
    }

    void UpdateBindsUI()
    {
        attack.text = binds.attack.ToString();
        interact.text = binds.interact.ToString();
        left.text = binds.moveLeft.ToString();
        right.text = binds.moveRight.ToString();
        forward.text = binds.moveForward.ToString();
        backward.text = binds.moveBackward.ToString();
    }

    public static void SetKey(KeyBind bindEditing)
    {
        singleton.isReadingKeys = !singleton.isReadingKeys;
        singleton.keyEditing = bindEditing;
    }
}
