using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public TalentTreeController talentTreeController;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void Update()
    {
        text.text = talentTreeController.playerContext.playerData.Money.ToString();
    }
}
