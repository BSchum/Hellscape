using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRoom : DefaultRoom
{
    public PlayerContext playerContext;
    public Transform holder;
    public GameObject bossDoor;
    [HideInInspector] List<Enemy> roomsEnemy;
    public Transform[] enemyHolders;
    [HideInInspector] public Door[] doors;
    [HideInInspector] public List<Enemy> enemies;
    [SerializeField] int enemyNumber;
    public bool roomCleared = false;
    public bool doorsClosed = false;
    public bool isBossEntranceRoom = false;
    public EnemyProvider enemyProvider;
    // Start is called before the first frame update
    void Start()
    {
        roomsEnemy = enemyProvider.GetRandomEnemies(enemyNumber);
        doors = GetComponentsInChildren<Door>();
        OpenDoors();
        SpawnEnemies();
        
    }

    private void FixedUpdate()
    {
        if (isBossEntranceRoom && playerContext.currentRoomNumber == roomNumber)
        {
            bossDoor.GetComponent<Animator>().SetTrigger("Open");
        }

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
        var doorsList = doors.OfType<Door>().ToList();
        doorsList.RemoveAll(door => door == null);

        foreach (var door in doorsList)
        {
            door.GetComponent<BoxCollider>().enabled = true;
            door.GetComponent<Animator>().SetTrigger("Close");

        }
        doorsClosed = true;
    }

    public void OpenDoors()
    {
        var doorsList = doors.OfType<Door>().ToList();
        doorsList.RemoveAll(door => door == null);
        foreach (var door in doorsList)
        {
            if(door != null)
            {
                door.GetComponent<BoxCollider>().enabled = false;
                door.GetComponent<Animator>().SetTrigger("Open");
            }

        }
        doorsClosed = false;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
