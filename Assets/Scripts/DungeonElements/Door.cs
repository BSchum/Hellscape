using UnityEngine;

public class Door : MonoBehaviour
{
    public SimpleRoom room;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == Constants.Tags.PLAYER_TAG && !room.roomCleared)
        {
            room.CloseDoors();
        }
    }
}
