using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    BaseStats currentStats = new BaseStats();
    BaseStats bonusStats = new BaseStats();
    BaseStats finalStats = new BaseStats();

    private void Update()
    {
        finalStats = currentStats + bonusStats;
    }

}
