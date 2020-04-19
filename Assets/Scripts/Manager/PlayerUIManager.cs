using SDG.Unity.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject ItemUI;
    [HideInInspector]
    public GameObject SoulUI;
    [HideInInspector]
    private GameObject PowerUI;
    [HideInInspector]
    public GameObject SpeedUI;
    public ItemUI itemUIPrefab;
    public SoulUI soulUIPrefab;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public GameObject HealthUI;
    private GameObject MaxHealthUI;
    public GameObject HeartPrefab;
    public GameObject MaxHealthPrefab;
    public bool IsUILoaded;
    public PlayerContext playerContext;

    public void Awake()
    {
        StartCoroutine(LoadUserInterfaceScene());
    }

    public void Start()
    {
        player = playerContext.player.GetComponent<Player>();
        player.Bag.OnAddItemEvent += Display;
        player.OnStatUpdateEvent += UpdateUI;
        player.Sword.OnSoulUpdateEvent += DisplaySouls;
        player.UpdateStatsUI();
    }
    private void Update()
    {
        if (player == null && playerContext.player != null && playerContext.player.GetComponent<Player>() != null)
        {
            player = playerContext.player.GetComponent<Player>();
        }
    }
    public void UpdateUI(Stats stats)
    {
        UpdateLifeUI(stats.Health, stats.MaxHealth);
        PowerUI.GetComponent<Text>().text = stats.Power.ToString();
        SpeedUI.GetComponent<Text>().text = stats.Speed.ToString();
    }
    void UpdateLifeUI(uint currentHealth, uint maxHealth)
    {
        HealthUI.transform.Clear();
        MaxHealthUI.transform.Clear();
        uint heartNumber;
        bool lastHeartIsHalf = false;

        //Calcul du bon nombre de coeur ( 2pv = 1coeur)
        if (currentHealth % 2 != 0)
        {
            heartNumber = currentHealth / 2 + 1;
            lastHeartIsHalf = true;
        }
        else
        {
            heartNumber = currentHealth / 2;
        }

        for (int i = 0; i < heartNumber; i++)
        {
            GameObject heart = Instantiate(HeartPrefab, HealthUI.transform);

            if (i == heartNumber - 1 && lastHeartIsHalf)
            {
                var halfHearts = heart.GetComponentsInChildren<HalfHeart>();
                halfHearts[halfHearts.Length - 1].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < Math.Ceiling(maxHealth / 2f); i++)
        {
            GameObject heartContainer = Instantiate(MaxHealthPrefab, MaxHealthUI.transform);
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
        SpeedUI = GameObject.Find("Speed");
        ItemUI = GameObject.Find("Items");
        SoulUI = GameObject.Find("Souls");
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
        GameObject newSoul = Instantiate(soulUIPrefab.gameObject, SoulUI.transform);
        newSoul.GetComponent<SoulUI>().image.sprite = soul.sprite;
    }
    private void OnDestroy()
    {
        player.Bag.OnAddItemEvent -= Display;
        player.OnStatUpdateEvent -= UpdateUI;
        player.Sword.OnSoulUpdateEvent -= DisplaySouls;
    }
}
