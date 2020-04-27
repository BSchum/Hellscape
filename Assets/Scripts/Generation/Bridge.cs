using SDG.Platform.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SDG.Unity.Scripts
{
    public class Bridge : MonoBehaviour
    {
        public Direction direction;
        public DefaultRoom room;
        public PlayerContext playerContext;
        public GameObject replacementWall;
        public GameObject brigeModel;
        private void OnTriggerEnter(Collider collider)
        {
            if(collider.tag == Constants.Tags.PLAYER_TAG)
            {
                if(playerContext.currentRoomNumber != room.roomNumber)
                    DungeonManager.GetInstance().MoveCameraToRoom(room.roomNumber);
                playerContext.currentRoomNumber = room.roomNumber;

            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.tag == Constants.Tags.PLAYER_TAG)
            {
                var simpleRoom = room.GetComponent<SimpleRoom>();
                if(simpleRoom != null && !simpleRoom.roomCleared)
                {
                    simpleRoom.CloseDoors();
                }
            }
        }
    }
}
