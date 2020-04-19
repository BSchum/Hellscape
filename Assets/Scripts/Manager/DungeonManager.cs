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
        public GameObject backGroundPrefab;
        public Transform roomParents;

        public PlayerContext playerContext;
        public int roomNumber;
        List<DefaultRoom> _rooms;
        Coroutine _currentTranslateCameraCoroutine;

        Camera _cam;
        // Start is called before the first frame update
        void Awake()
        {
            _cam = Camera.main;
            RandomProvider randomProvider = new RandomProvider();
            var generator = new DungeonGenerator(randomProvider);
            var dungeon = generator.Generate(roomNumber);
            var specialRoomList = new List<IRoomBehaviour>()
            {
                new StartRoomBehaviour(randomProvider),
                new TreasureRoomBehaviour(randomProvider),
                new BossRoomBehaviour(randomProvider)
            };

            dungeon = generator.PopulateRooms(dungeon, specialRoomList);

            Generate3DDungeon(dungeon);
            GenerateDungeonBackground(dungeon);
        }
        void Generate3DDungeon(Dungeon dungeon)
        {
            _rooms = new List<DefaultRoom>();
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
                        if (playerContext.player == null)
                            playerContext.player = Instantiate(playerPrefab, playerHolder.transform.position, Quaternion.identity) as GameObject;
                        else
                            playerContext.player.transform.position = playerHolder.transform.position;
                        Debug.Log("The player is " + playerContext.player);

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

                    _rooms.Add(defaultRoom);

                    instantiateRoom.name = $"Room({room.Pos.X},{room.Pos.Y}) Number {i}";
                    //Destruction des ponts
                    var bridges = instantiateRoom.GetComponentsInChildren<Bridge>().OfType<Bridge>().ToList();
                    foreach (Bridge bridge in bridges)
                    {
                        if (!room.OpenedDoors[bridge.direction])
                        {
                            Destroy(bridge.gameObject);
                        }
                    }

                    if (room.RoomType == RoomType.Start)
                    {
                        playerContext.currentRoomNumber = i;
                        MoveCameraToRoom(defaultRoom.roomNumber);
                    }
                }

                i++;
            }
        }
        void GenerateDungeonBackground(Dungeon dungeon)
        {
            int minIndexX = 999;
            int minIndexY = 999;
            int maxIndexX = 0;
            int maxIndexY = 0;
            foreach (Room room in dungeon.dungeon)
            {
                if (room.RoomType != RoomType.None)
                {
                    minIndexX = Mathf.Min(minIndexX, room.Pos.X);
                    maxIndexX = Mathf.Max(maxIndexX, room.Pos.X);

                    minIndexY = Mathf.Min(minIndexY, room.Pos.Y);
                    maxIndexY = Mathf.Max(maxIndexY, room.Pos.Y);
                }
            }

            //Mon nombre de salle présente
            int XRoomNumber = maxIndexX + 1 - minIndexX;
            int YRoomNumber = maxIndexY + 1 - minIndexY;
            //Nombre de background : Nombre de room * taille des room / taille du background -> Nombre de background;
            int XPlaneNumber = Mathf.CeilToInt(XRoomNumber * Constants.Rooms.ROOM_SIZE_X / Constants.Rooms.ROOM_BACKGROUND_SIZE_X); ;
            int YPlaneNumber = Mathf.CeilToInt(YRoomNumber * Constants.Rooms.ROOM_SIZE_Y / Constants.Rooms.ROOM_BACKGROUND_SIZE_Y);

            float XPlaneNumberOffset = minIndexX * Constants.Rooms.ROOM_SIZE_X / Constants.Rooms.ROOM_BACKGROUND_SIZE_X;
            float YPlaneNumberOffset = minIndexY * Constants.Rooms.ROOM_SIZE_Y / Constants.Rooms.ROOM_BACKGROUND_SIZE_Y;

            for (float x = XPlaneNumberOffset - 1; x < XPlaneNumber + XPlaneNumberOffset + 1; x++)
            {
                for (float y = YPlaneNumberOffset - 1; y < YPlaneNumber + YPlaneNumberOffset + 1; y++)
                {
                    Instantiate(backGroundPrefab, new Vector3(x * Constants.Rooms.ROOM_BACKGROUND_SIZE_X, backGroundPrefab.transform.position.y, y * Constants.Rooms.ROOM_BACKGROUND_SIZE_Y), Quaternion.identity, roomParents);
                }
            }
        }

        public void MoveCameraToRoom(int roomNumber)
        {
            Debug.Log($"On va vers la room numero ->{roomNumber}");
            var camHolder = _rooms.Where(r => r.roomNumber == roomNumber).First().cameraHolder;
            if (_currentTranslateCameraCoroutine != null)
                StopCoroutine(_currentTranslateCameraCoroutine);
            _currentTranslateCameraCoroutine = StartCoroutine(TranslateCameraTo(camHolder.position));
        }

        public IEnumerator TranslateCameraTo(Vector3 position)
        {
            float elapsedTime = 0;
            while (elapsedTime < 2)
            {
                _cam.transform.position = Vector3.Lerp(_cam.transform.position, position, elapsedTime / 2);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
