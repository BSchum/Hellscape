using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] TextMeshProUGUI text;

    public void Update()
    {
        text.text = "Money : " + playerData.Money;
    }
}
