﻿using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void Update()
    {
        text.text = "Money : " + playerData.Money.ToString();
    }
}
