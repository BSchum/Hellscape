using SDG.Unity.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerUIManager : ManagerSingleton<PlayerUIManager>
{
    public ItemUI itemUIPrefab;
    public Transform bagAnchor;
    public Player player;
    public GameObject HealthUI;
    public GameObject HeartPrefab;
    public bool IsUILoaded;

    public void Awake()
    {
        StartCoroutine(LoadUserInterfaceScene());
    }

    public void Start()
    {
        player = PlayerContext.instance.player.GetComponent<Player>();
        player.OnTakeDamageEvent += UpdateLifeUI;
        player.Bag.OnAddItemEvent += Display;
    }
    private void Update()
    {

        if (PlayerContext.instance.player.GetComponent<Player>() != null && player == null)
        {
            player = PlayerContext.instance.player.GetComponent<Player>();
        }

    }

    public void UpdateLifeUI(int currentHealth)
    {
        HealthUI.transform.Clear();
        int heartNumber;
        bool lastIsHalf = false;
        if(currentHealth % 2 != 0)
        {
            heartNumber = currentHealth / 2 + 1;
            lastIsHalf = true;
        }
        else
        {
            heartNumber = currentHealth;
        }

        //On fait spawn le bon nombre de coeur
        for (int i =0; i < heartNumber; i++)
        {
            GameObject heart = Instantiate(HeartPrefab, HealthUI.transform);

            if (i == heartNumber - 1 && lastIsHalf)
            {
                var halfHearts = heart.GetComponentsInChildren<HalfHeart>();
                halfHearts[halfHearts.Length -1].gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator LoadUserInterfaceScene()
    {
        AsyncOperation UI = SceneManager.LoadSceneAsync("UserInterface", LoadSceneMode.Additive);
        while (!UI.isDone)
        {
            yield return null;
        }

        HealthUI = GameObject.Find("LifeView");
        IsUILoaded = true;
        player.TakeDamage(0);
    }
    public void Display(Item item)
    {
        GameObject newItem = Instantiate(itemUIPrefab.gameObject, bagAnchor);
        newItem.GetComponent<ItemUI>().image.sprite = item.sprite;
    }
}
