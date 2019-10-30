using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image image;

    public void DisplayItem(Item item)
    {
        image.sprite = item.sprite;
    }
}
