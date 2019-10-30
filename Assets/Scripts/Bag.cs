using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
    }
}
