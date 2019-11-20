using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProvider : MonoBehaviour
{
    public List<Enemy> enemies;

    private static EnemyProvider _instance;

    public static EnemyProvider Instance
    {
        get { return _instance; }
    }
    public EnemyProvider()
    {
        _instance = this;
    }
    // Start is called before the first frame update

    public List<Enemy> GetRandomEnemies(int enemyNumber)
    {
        List<Enemy> roomEnemies = new List<Enemy>();
        for(int i =0; i < enemyNumber; i++)
        {
            roomEnemies.Add(enemies[Random.Range(0, enemies.Count)]);
        }

        return roomEnemies;
    }
}
