using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyProvider", menuName = "Providers/EnemyProvider")]
public class EnemyProvider : ScriptableObject
{
    public List<Enemy> enemies;
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
