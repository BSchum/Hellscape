using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    private Item _item;
    public GameObject anchor;
    // Start is called before the first frame update
    void Start()
    {
        _item = ItemsProvider.Instance.GetRandomItem();
        Instantiate(_item, anchor.transform.position, Quaternion.identity);
    }

}
