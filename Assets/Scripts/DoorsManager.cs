using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManager : MonoBehaviour
{
    Door[] doors;
    private void Start()
    {
        doors = GetComponentsInChildren<Door>();
    }
    public void CloseDoors()
    {
        foreach(var door in doors)
        {
            door.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            door.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            door.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
