using SDG.Platform.Entities;
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

        public void Reset()
        {
            player = null;
            currentRoomNumber = 0;
            currentPosition = null;
        }
    }
}