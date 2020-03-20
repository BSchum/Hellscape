using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private Item _item;
    public GameObject anchor;
    private GameObject _itemSpawned;
    public bool IsOpened { get; private set; }
    public bool IsLooted { get; private set; }

    public ItemsProvider itemsProvider;
    private bool isOpening;

    void Start()
    {
        _item = itemsProvider.GetRandomItem();
    }

    public void Open()
    {
        if (!isOpening)
        {
            _animator.SetTrigger("Open");
        }

        isOpening = true;
    }

    private void ReleaseObject()
    {
        IsOpened = true;
        _itemSpawned = Instantiate(_item.gameObject, anchor.transform.position, Quaternion.identity, anchor.transform);
        _itemSpawned.SetActive(true);
    }

    public Item GetItem()
    {
        _itemSpawned.gameObject.SetActive(false);
        IsLooted = true;
        return _item;
    }
}
