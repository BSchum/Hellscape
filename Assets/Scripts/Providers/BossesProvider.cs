using System.Collections.Generic;
using UnityEngine;

public class BossesProvider : MonoBehaviour
{
    public List<Boss> _hellBosses;
    private static BossesProvider _instance;

    public static BossesProvider Instance
    {
        get { return _instance; }
    }
    public BossesProvider()
    {
        _instance = this;
    }
    public Boss GetRandomBosses()
    {
        return _hellBosses[Random.Range(0, _hellBosses.Count)];
    }
}
