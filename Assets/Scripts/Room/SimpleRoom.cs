using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoom : MonoBehaviour
{
    public Transform holder;
    List<Enemy> roomsEnemy;
    public Transform[] enemyHolders;
    public Door[] doors;
    public List<Enemy> enemies;
    [SerializeField]
    int enemyNumber;
    // Start is called before the first frame update
    void Start()
    {
        roomsEnemy = EnemyProvider.Instance.GetRandomEnemies(enemyNumber);
        doors = GetComponentsInChildren<Door>();
        SpawnEnemies();
    }

    private void FixedUpdate()
    {
        enemies.RemoveAll(enemy => enemy == null);
        if(enemies.Count == 0)
        {
            OpenDoors();
        }
    }
    void SpawnEnemies()
    {
        for(int i = 0; i < roomsEnemy.Count; i++)
        {
            var enemy = Instantiate(roomsEnemy[i], enemyHolders[i].position, Quaternion.identity);
            enemies.Add(enemy);
        }
    }

    public void CloseDoors()
    {
        foreach (var door in doors)
        {
            door.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            door.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            door.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
