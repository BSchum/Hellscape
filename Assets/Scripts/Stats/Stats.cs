using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    BaseStats currentStats;
    BaseStats bonusStats;
    BaseStats finalStats;

    private void Update()
    {
        finalStats = currentStats + bonusStats;
    }

}
