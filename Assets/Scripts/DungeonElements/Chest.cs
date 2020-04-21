using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private Item _item;
    public GameObject anchor;
    public Text text;
    public Transform textAnchor;
    private GameObject _itemSpawned;

    public bool IsOpened { get; private set; }
    public bool IsLooted { get; private set; }

    public ItemsProvider itemsProvider;
    private bool isOpening;

    void Start()
    {
        _item = itemsProvider.GetRandomItem();
    }

    private void Update()
    {
        text.transform.position = Camera.main.WorldToScreenPoint(textAnchor.transform.position);
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

    public void ToggleUI(bool toggle)
    {
        text.enabled = toggle;
    }
}
