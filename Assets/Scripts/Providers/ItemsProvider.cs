using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsProvider : MonoBehaviour
{
    public List<Item> hellItems;
    private static ItemsProvider _instance;

    public static ItemsProvider Instance
    {
        get { return _instance; }
    }

    public ItemsProvider()
    {
        _instance = this;
    }
    public Item GetRandomItem()
    {
        return hellItems[Random.Range(0, hellItems.Count)];
    }
}
