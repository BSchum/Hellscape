using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite sprite;
    private float _speed = 20f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * _speed * Time.deltaTime);
    }
}
