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
    public class DungeonManager : ManagerSingleton<DungeonManager>
    {
        public GameObject defaultRoomPrefab;
        public GameObject treasureRoomPrefab;
        public GameObject bossRoomPrefab;
        public GameObject startRoomPrefab;
        public GameObject playerPrefab;

        public Transform roomParents;

        public PlayerContext playerContext;
        public int roomNumber;
        List<DefaultRoom> rooms;

        Camera cam;
        // Start is called before the first frame update
        void Awake()
        {
            playerContext = GetComponent<PlayerContext>();
            cam = Camera.main;
            RandomProvider randomProvider = new RandomProvider();
            var generator = new DungeonGenerator(randomProvider);
            var dungeon = generator.Generate(roomNumber);
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
            rooms = new List<DefaultRoom>();
            int i = 0;
            foreach (Room room in dungeon.dungeon)
            {
                GameObject instantiateRoom = null;
                switch (room.RoomType)
                {
                    case RoomType.Default:
                        instantiateRoom = Instantiate(defaultRoomPrefab, new Vector3(room.Pos.X * Constants.Rooms.ROOM_SIZE_X, 0, room.Pos.Y * Constants.Rooms.ROOM_SIZE_Y), Quaternion.identity, roomParents);
                        break;
                    case RoomType.Treasure:
                        instantiateRoom = Instantiate(treasureRoomPrefab, new Vector3(room.Pos.X * Constants.Rooms.ROOM_SIZE_X, 0, room.Pos.Y * Constants.Rooms.ROOM_SIZE_Y), Quaternion.identity, roomParents);
                        break;
                    case RoomType.Boss:
                        instantiateRoom = Instantiate(bossRoomPrefab, new Vector3(room.Pos.X * Constants.Rooms.ROOM_SIZE_X, 0, room.Pos.Y * Constants.Rooms.ROOM_SIZE_Y), Quaternion.identity, roomParents);
                        break;
                    case RoomType.Start:
                        instantiateRoom = Instantiate(startRoomPrefab, new Vector3(room.Pos.X * Constants.Rooms.ROOM_SIZE_X, 0, room.Pos.Y * Constants.Rooms.ROOM_SIZE_Y), Quaternion.identity, roomParents);
                        var playerHolder = instantiateRoom.GetComponent<StartRoom>().playerHolder;
                        playerContext.player = Instantiate(playerPrefab, playerHolder.transform.position, Quaternion.identity) as GameObject;
                        playerContext.currentPosition = room.Pos;
                        DontDestroyOnLoad(playerContext.player);
                        break;
                    default:
                        break;
                }

                if (instantiateRoom != null)
                {
                    var defaultRoom = instantiateRoom.GetComponent<DefaultRoom>();
                    defaultRoom.roomNumber = i;
                    
                    rooms.Add(defaultRoom);

                    instantiateRoom.name = $"Room({room.Pos.X},{room.Pos.Y}) Number {i}";
                    //Destruction des ponts
                    var bridges = instantiateRoom.GetComponentsInChildren<Bridge>().OfType<Bridge>().ToList();
                    foreach(Bridge bridge in bridges)
                    {
                        if (!room.OpenedDoors[bridge.direction])
                        {
                            Destroy(bridge.gameObject);
                        }
                    }

                    if(room.RoomType == RoomType.Start)
                    {
                        PlayerContext.instance.currentRoomNumber = i;
                        MoveCameraToRoom(defaultRoom.roomNumber);
                    }
                }

                i++;
            } 
        }
       
        
        public void MoveCameraToRoom(int roomNumber)
        {
            Debug.Log($"On va vers la room numero ->{roomNumber}");
            var camHolder = rooms.Where(r => r.roomNumber == roomNumber).First().cameraHolder;
            StartCoroutine(TranslateCameraTo(camHolder.position));
            
        }

        public IEnumerator TranslateCameraTo(Vector3 position)
        {
            float elapsedTime = 0;
            while (elapsedTime < 2)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, position, elapsedTime / 2);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
