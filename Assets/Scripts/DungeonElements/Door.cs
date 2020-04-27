using SDG.Platform.Entities;
using UnityEngine;

public class Door : MonoBehaviour
{
    public SimpleRoom room;
    public Direction direction;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == Constants.Tags.PLAYER_TAG && !room.roomCleared)
        {
            //room.CloseDoors();
        }
    }
}
