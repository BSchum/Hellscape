using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoom : DefaultRoom
{
    public Transform holder;

    [HideInInspector] List<Enemy> roomsEnemy;
    public Transform[] enemyHolders;
    [HideInInspector] public Door[] doors;
    [HideInInspector] public List<Enemy> enemies;
    [SerializeField] int enemyNumber;
    public bool roomCleared = false;
    public bool doorsClosed = false;
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
            roomCleared = true;
        }
    }
    void SpawnEnemies()
    {
        for(int i = 0; i < roomsEnemy.Count; i++)
        {
            var enemy = Instantiate(roomsEnemy[i], enemyHolders[i].position, Quaternion.identity);
            enemy.roomNumber = roomNumber;
            enemy.room = this;
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
        doorsClosed = true;
    }

    public void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            door.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        doorsClosed = false;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
