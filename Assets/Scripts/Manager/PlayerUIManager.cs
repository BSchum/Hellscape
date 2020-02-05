using SDG.Unity.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUIManager : ManagerSingleton<PlayerUIManager>
{
    public GameObject ItemUI;
    public ItemUI itemUIPrefab;
    public SoulUI soulUIPrefab;
    public Player player;
    public GameObject HealthUI;
    public GameObject PowerUI;
    public GameObject MaxHealthUI;
    public GameObject HeartPrefab;
    public GameObject MaxHealthPrefab;
    public GameObject PowerPrefab;
    public bool IsUILoaded;

    public void Awake()
    {
        StartCoroutine(LoadUserInterfaceScene());
    }

    public void Start()
    {
        player = PlayerContext.instance.player.GetComponent<Player>();
        player.Bag.OnAddItemEvent += Display;
        player.Sword.OnSoulUpdateEvent += DisplaySouls;
        player.OnStatUpdateEvent += UpdateUI;
    }
    private void Update()
    {
        if (player == null && PlayerContext.instance.player != null && PlayerContext.instance.player.GetComponent<Player>() != null)
        {
            player = PlayerContext.instance.player.GetComponent<Player>();
        }
    }
    public void UpdateUI(Stats stats)
    {
        UpdateLifeUI(stats.Health, stats.MaxHealth);
        UpdatePowerUI(stats.Power);
    }
    void UpdateLifeUI(uint currentHealth, uint maxHealth)
    {
        HealthUI.transform.Clear();
        MaxHealthUI.transform.Clear();
        uint heartNumber;
        bool lastHeartIsHalf = false;

        //Calcul du bon nombre de coeur ( 2pv = 1coeur)
        if(currentHealth % 2 != 0)
        {
            heartNumber = currentHealth / 2 + 1;
            lastHeartIsHalf = true;
        }
        else
        {
            heartNumber = currentHealth/2;
        }

        for (int i =0; i < heartNumber; i++)
        {
            GameObject heart = Instantiate(HeartPrefab, HealthUI.transform);

            if (i == heartNumber - 1 && lastHeartIsHalf)
            {
                var halfHearts = heart.GetComponentsInChildren<HalfHeart>();
                halfHearts[halfHearts.Length -1].gameObject.SetActive(false);
            }
        }

        for(int i=0; i < Math.Ceiling(maxHealth /2f); i++)
        {
            GameObject heartContainer = Instantiate(MaxHealthPrefab, MaxHealthUI.transform);
        }
    }
    void UpdatePowerUI(uint powerNumber)
    {
        PowerUI.transform.Clear();
        for(int i = 0; i < powerNumber; i++)
        {
            GameObject power = Instantiate(PowerPrefab, PowerUI.transform);
        }
    }
    private IEnumerator LoadUserInterfaceScene()
    {
        AsyncOperation UI = SceneManager.LoadSceneAsync("UserInterface", LoadSceneMode.Additive);
        while (!UI.isDone)
        {
            yield return null;
        }

        HealthUI = GameObject.Find("Health");
        MaxHealthUI = GameObject.Find("MaxHealth");
        PowerUI = GameObject.Find("Power");
        ItemUI = GameObject.Find("Items");
        IsUILoaded = true;

        UpdateUI(player.stats);
    }
    public void Display(Item item)
    {
        GameObject newItem = Instantiate(itemUIPrefab.gameObject, ItemUI.transform);
        newItem.GetComponent<ItemUI>().image.sprite = item.sprite;
    }
    private void DisplaySouls(Soul soul)
    {
        GameObject newItem = Instantiate(soulUIPrefab.gameObject, ItemUI.transform);
        newItem.GetComponent<ItemUI>().image.sprite = soul.sprite;
    }
}
