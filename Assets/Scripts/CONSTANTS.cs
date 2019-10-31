using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static class Tags
    {
        public const string ENEMY_TAG = "Enemy";
        public const string BRIDGE_TAG = "Bridge";
        public const string PLAYER_TAG = "Player";
    }

    public static class Inputs
    {
        public const string PLAYER_HORIZONTAL = "Horizontal";
        public const string PLAYER_VERTICAL = "Vertical";
        public const string PLAYER_HIT = "Hit";
    }

    public static class Rooms
    {
        public const float ROOM_SIZE_X = 30f + 6f;
        public const float ROOM_SIZE_Y = 20f + 6f;
    }

    public static class Enemies
    {
        public static class LittleDoggo
        {
            public const float ATTACK_RADIUS = 20f;
        }
    }

}