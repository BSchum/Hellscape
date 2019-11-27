using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoom : MonoBehaviour
{
    public Transform holder;
    List<Enemy> roomsEnemy;
    public Transform[] enemyHolders;
    
    [SerializeField]
    int enemyNumber;
    // Start is called before the first frame update
    void Start()
    {
        roomsEnemy = EnemyProvider.Instance.GetRandomEnemies(enemyNumber);
        SpawnEnemies();
    }
    
    void SpawnEnemies()
    {
        for(int i = 0; i < roomsEnemy.Count; i++)
        {
            Instantiate(roomsEnemy[i], enemyHolders[i].position, Quaternion.identity);
        }
    }
}
