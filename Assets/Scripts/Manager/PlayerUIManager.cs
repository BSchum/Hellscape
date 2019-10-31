using SDG.Unity.Scripts;
using UnityEngine;
public class PlayerUIManager : MonoBehaviour
{
    public ItemUI itemUIPrefab;
    public Transform bagAnchor;
    public Player player;

    public void Awake()
    {

    }

    private void Update()
    {
        if (PlayerContext.instance.player.GetComponent<Player>() != null && player == null)
        {
            player = PlayerContext.instance.player.GetComponent<Player>();
            player.Bag.OnAddItemEvent += Display;
        }
    }

    public void Display(Item item)
    {
        GameObject newItem = Instantiate(itemUIPrefab.gameObject, bagAnchor);
        newItem.GetComponent<ItemUI>().image.sprite = item.sprite;
    }
}
