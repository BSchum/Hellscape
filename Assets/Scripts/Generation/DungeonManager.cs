﻿using System.Collections;
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

        private const float ROOM_SIZE_SCALE_X = 30f + 6f;
        private const float ROOM_SIZE_SCALE_Y = 20f + 6f;
        // Start is called before the first frame update
        void Start()
        {
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
                        instantiateRoom = Instantiate(defaultRoomPrefab, new Vector3(room.Pos.X * ROOM_SIZE_SCALE_X, 0, room.Pos.Y * ROOM_SIZE_SCALE_Y), Quaternion.identity);
                        break;
                    case RoomType.Treasure:
                        instantiateRoom = Instantiate(treasureRoomPrefab, new Vector3(room.Pos.X * ROOM_SIZE_SCALE_X, 0, room.Pos.Y * ROOM_SIZE_SCALE_Y), Quaternion.identity);
                        break;
                    case RoomType.Boss:
                        instantiateRoom = Instantiate(bossRoomPrefab, new Vector3(room.Pos.X * ROOM_SIZE_SCALE_X, 0, room.Pos.Y * ROOM_SIZE_SCALE_Y), Quaternion.identity);
                        break;
                    case RoomType.Start:
                        instantiateRoom = Instantiate(startRoomPrefab, new Vector3(room.Pos.X * ROOM_SIZE_SCALE_X, 0, room.Pos.Y * ROOM_SIZE_SCALE_Y), Quaternion.identity);
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

        }
    }

}
