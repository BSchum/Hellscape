using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public ItemUI itemUIPrefab;
    public Transform bagAnchor;

    public delegate void PlayerUIUpdate(Item item);
    public static PlayerUIUpdate playerUIUpdate;

    public void Start()
    {
        playerUIUpdate += Display;
    }

    public void Display(Item item)
    {
        GameObject newItem = Instantiate(itemUIPrefab.gameObject, bagAnchor);
        newItem.GetComponent<ItemUI>().DisplayItem(item);
    }
}
