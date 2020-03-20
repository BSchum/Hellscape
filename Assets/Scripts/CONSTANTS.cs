using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public class Tags
    {
        public const string ENEMY_TAG = "Enemy";
        public const string BRIDGE_TAG = "Bridge";
        public const string CHEST_TAG = "Chest";
        public const string PLAYER_TAG = "Player";
        public const string FLOOR_TAG = "Floor";
    }

    public class Rooms
    {
        public const float ROOM_SIZE_X = 50f + 8f;
        public const float ROOM_SIZE_Y = 30f + 8f;
        public const float ROOM_BACKGROUND_SIZE_X = 60f;
        public const float ROOM_BACKGROUND_SIZE_Y = 60f;
    }

}