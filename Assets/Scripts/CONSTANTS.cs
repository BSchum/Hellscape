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

    public class Inputs
    {
        public const string PLAYER_HORIZONTAL = "Horizontal";
        public const string PLAYER_VERTICAL = "Vertical";
        public const string PLAYER_HIT = "Hit";
        public const string PLAYER_INTERACT = "Interact";
    }

    public class Rooms
    {
        public const float ROOM_SIZE_X = 50f + 6f;
        public const float ROOM_SIZE_Y = 30f + 6f;
    }

}