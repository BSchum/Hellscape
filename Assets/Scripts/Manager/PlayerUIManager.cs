﻿using SDG.Unity.Scripts;
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
    private GameObject LevelNumberUI;
    private GameObject GoldEarnedUI;
    private GameObject InGameGoldEarnedUI;
    private GameObject DeadMenuUI;
    private GameObject PauseMenuUI;
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
        player.OnStatUpdateEvent += UpdateStatsUI;
        player.Sword.OnSoulUpdateEvent += DisplaySouls;
        playerContext.OnGoldEarnedEvent += UpdateGoldUI;

        StartCoroutine(PrepareUI());
    }
    private void Update()
    {
        if (player == null && playerContext.player != null && playerContext.player.GetComponent<Player>() != null)
        {
            player = playerContext.player.GetComponent<Player>();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuUI.SetActive(true);
        }
    }
    IEnumerator PrepareUI()
    {
        while (!IsUILoaded)
        {
            yield return null;
        }

        Debug.Log("Update stats");
        UpdateStatsUI(player.stats);
        UpdateGoldUI();
        LevelNumberUI.GetComponent<Text>().text = playerContext.currentLevel.ToString();
        foreach (var item in player.Bag.GetAllCurrentItems())
        {
            Display(item);
        }

        foreach(var soul in player.Sword.souls)
        {
            DisplaySouls(soul);
        }
    }

    public void UpdateGoldUI()
    {
        InGameGoldEarnedUI.GetComponent<Text>().text = playerContext.goldEarned.ToString();
        GoldEarnedUI.GetComponent<Text>().text = playerContext.goldEarned.ToString();
    }

    public void UpdateStatsUI(Stats stats)
    {
        UpdateLifeUI(stats.Health, stats.MaxHealth);
        PowerUI.GetComponent<Text>().text = stats.Power.ToString();
        SpeedUI.GetComponent<Text>().text = stats.Speed.ToString();

        if(stats.Health <= 0)
        {
            ShowDeadPanel();
        }
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
        LevelNumberUI = GameObject.Find("LevelNumber");
        DeadMenuUI = GameObject.Find("DeadMenu");
        PauseMenuUI = GameObject.Find("PauseMenu");
        GoldEarnedUI = GameObject.Find("EarnedGold");
        InGameGoldEarnedUI = GameObject.Find("Gold");
        DeadMenuUI.SetActive(false);
        PauseMenuUI.SetActive(false);

        IsUILoaded = true;
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

    public void ShowDeadPanel()
    {
        DeadMenuUI.SetActive(true);
    }
    private void OnDestroy()
    {
        player.Bag.OnAddItemEvent -= Display;
        player.OnStatUpdateEvent -= UpdateStatsUI;
        player.Sword.OnSoulUpdateEvent -= DisplaySouls;
        playerContext.OnGoldEarnedEvent -= UpdateGoldUI;
    }
}
