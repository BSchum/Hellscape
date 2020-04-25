using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAllData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.LoadData<PlayerData>(SaveSystem.Data.PlayerData);
        SaveSystem.LoadData<KeyBindData>(SaveSystem.Data.Inputs);
        SaveSystem.LoadData<GraphicSetting>(SaveSystem.Data.GraphicSetting);
        SaveSystem.LoadData<SoundData>(SaveSystem.Data.Sounds);
        SaveSystem.LoadData<TalentData>(SaveSystem.Data.Talents);
    }

}
