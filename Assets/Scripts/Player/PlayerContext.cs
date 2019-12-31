using SDG.Platform.Entities;
using UnityEngine;

namespace SDG.Unity.Scripts
{
    /// <summary>
    /// Hold every player data that's important for the current level.
    /// </summary>
    public class PlayerContext : MonoBehaviour
    {
        public Position currentPosition;
        public int currentRoomNumber;
        [HideInInspector]
        public GameObject player;
        public static PlayerContext instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

    }
}