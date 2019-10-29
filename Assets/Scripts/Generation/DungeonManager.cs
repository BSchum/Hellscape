using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDG.Platform;
using SDG.Platform.Provider;
using SDG.Platform.Behaviours;
using SDG.Platform.Entities;
using System.Linq;

namespace SDG.Unity.Scripts
{
    public class DungeonManager : MonoBehaviour
    {
        public GameObject defaultRoomPrefab;
        public GameObject treasureRoomPrefab;
        public GameObject bossRoomPrefab;
        public GameObject startRoomPrefab;
        public GameObject playerPrefab;

        public PlayerContext playerContext;

        Camera camera;
        // Start is called before the first frame update
        void Start()
        {
            playerContext = GetComponent<PlayerContext>();
            camera = Camera.main;
            RandomProvider randomProvider = new RandomProvider();
            var generator = new DungeonGenerator(randomProvider);
            var dungeon = generator.Generate();
            var specialRoomList = new List<IRoomBehaviour>()
            {
                new StartRoomBehaviour(randomProvider),
                new TreasureRoomBehaviour(randomProvider),
                new BossRoomBehaviour(randomProvider),
            };

            dungeon = generator.PopulateRooms(dungeon, specialRoomList);
            GenerateDungeon(dungeon);
        }
        void GenerateDungeon(Dungeon dungeon)
        {
            foreach (Room room in dungeon.dungeon)
            {
                GameObject instantiateRoom = null;
                switch (room.RoomType)
                {
                    case RoomType.Default:
                        instantiateRoom = Instantiate(defaultRoomPrefab, new Vector3(room.Pos.X * Constants.Rooms.ROOM_SIZE_X, 0, room.Pos.Y * Constants.Rooms.ROOM_SIZE_Y), Quaternion.identity);
                        break;
                    case RoomType.Treasure:
                        instantiateRoom = Instantiate(treasureRoomPrefab, new Vector3(room.Pos.X * Constants.Rooms.ROOM_SIZE_X, 0, room.Pos.Y * Constants.Rooms.ROOM_SIZE_Y), Quaternion.identity);
                        break;
                    case RoomType.Boss:
                        instantiateRoom = Instantiate(bossRoomPrefab, new Vector3(room.Pos.X * Constants.Rooms.ROOM_SIZE_X, 0, room.Pos.Y * Constants.Rooms.ROOM_SIZE_Y), Quaternion.identity);
                        break;
                    case RoomType.Start:
                        instantiateRoom = Instantiate(startRoomPrefab, new Vector3(room.Pos.X * Constants.Rooms.ROOM_SIZE_X, 0, room.Pos.Y * Constants.Rooms.ROOM_SIZE_Y), Quaternion.identity);
                        var playerHolder = instantiateRoom.GetComponent<StartRoom>().playerHolder;
                        playerContext.player = Instantiate(playerPrefab, playerHolder.transform.position, Quaternion.identity) as GameObject;
                        playerContext.currentPosition = room.Pos;
                        break;
                    default:
                        break;
                }
                if (instantiateRoom != null)
                {
                    instantiateRoom.name = $"Room({room.Pos.X},{room.Pos.Y})";

                    var bridges = instantiateRoom.GetComponentsInChildren<Bridge>().OfType<Bridge>().ToList();
                    foreach(Bridge bridge in bridges)
                    {
                        if (!room.OpenedDoors[bridge.direction])
                        {
                            Destroy(bridge.gameObject);
                        }
                    }
                }

            }
        }
        // Update is called once per frame
        void Update()
        {
            MoveCamera();
        }
        public void MoveCamera()
        {
            var playerPosition = playerContext.player.transform.position;
            
            if (playerPosition.x >= (playerContext.currentPosition.X * Constants.Rooms.ROOM_SIZE_X) + Constants.Rooms.ROOM_SIZE_X / 2)
            {
                playerContext.currentPosition.X += 1;
                StartCoroutine(TranslateCamera(new Vector3(Constants.Rooms.ROOM_SIZE_X, 0, 0)));
            }
            else if (playerPosition.x <= (playerContext.currentPosition.X * Constants.Rooms.ROOM_SIZE_X) - Constants.Rooms.ROOM_SIZE_X / 2)
            {
                playerContext.currentPosition.X -= 1;
                StartCoroutine(TranslateCamera(new Vector3(-Constants.Rooms.ROOM_SIZE_X, 0, 0)));
            }
            else if (playerPosition.z >= (playerContext.currentPosition.Y * Constants.Rooms.ROOM_SIZE_Y) + Constants.Rooms.ROOM_SIZE_Y / 2)
            {
                playerContext.currentPosition.Y += 1;
                StartCoroutine(TranslateCamera(new Vector3(0, 0, Constants.Rooms.ROOM_SIZE_Y)));
            }
            else if (playerPosition.z <= (playerContext.currentPosition.Y * Constants.Rooms.ROOM_SIZE_Y) - Constants.Rooms.ROOM_SIZE_Y / 2)
            {
                playerContext.currentPosition.Y -= 1;
                StartCoroutine(TranslateCamera(new Vector3(0, 0, -Constants.Rooms.ROOM_SIZE_Y)));
            }
        }
        

        public IEnumerator TranslateCamera(Vector3 distance)
        {
            var endPoint = camera.transform.position + distance;

            float elapsedTime = 0;
            while(elapsedTime < 5)
            {
                camera.transform.position = Vector3.Lerp(camera.transform.position, endPoint, elapsedTime/5);
                elapsedTime += Time.deltaTime;
                yield return null;
            }


        }
    }

}
