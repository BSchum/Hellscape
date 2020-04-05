using SDG.Platform.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unity.Scripts
{
    /// <summary>
    /// Hold every player data that's important for the current level.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerContextData", menuName ="Player/PlayerData")]
    public class PlayerContext : ScriptableObject
    {
        public Position currentPosition;
        public int currentRoomNumber;
        public GameObject player;
        public PlayerData playerData;

        public void Reset()
        {
            player = null;
            currentRoomNumber = 0;
            currentPosition = null;
        }
    }
}