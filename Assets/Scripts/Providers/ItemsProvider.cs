using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemProvider", menuName = "Items/ItemProvider")]
public class ItemsProvider : ScriptableObject
{
    public List<Item> hellItems;

    public Item GetRandomItem()
    {
        return hellItems[Random.Range(0, hellItems.Count)];
    }
}
