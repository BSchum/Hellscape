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

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.tag == Constants.Tags.PLAYER_TAG)
            {
                if(PlayerContext.instance.currentRoomNumber != room.roomNumber)
                    DungeonManager.GetInstance().MoveCameraToRoom(room.roomNumber);
                PlayerContext.instance.currentRoomNumber = room.roomNumber;

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
