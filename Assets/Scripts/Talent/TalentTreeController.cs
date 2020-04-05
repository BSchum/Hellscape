using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using SDG.Unity.Scripts;

public class TalentTreeController : MonoBehaviour
{
    public PlayerContext playerContext;
    public TalentData talentData;
    [SerializeField] private TreeSet[] lines;

    void Start()
    {
        talentData = SaveSystem.LoadData<TalentData>(SaveSystem.Data.Talents);

        // First time we load the game the data is empty
        if (talentData.talents.Count > 0)
        {
            for (int i = 0; i < talentData.talents.Count; i++)
                lines[i].talentUI.talent.SetDataFromSerialize(talentData.talents[i]);
        }

        foreach (TreeSet line in lines)
        {
            line.talentUI.talentTreeController = this;
        }

        var tempPlayerData = SaveSystem.LoadData<PlayerData>(SaveSystem.Data.PlayerData);
        if (tempPlayerData != null)
        {
            playerContext.playerData = tempPlayerData;
        }

        RefreshTalentTree();
    }

    public void OnClickTalent(TalentUI talentUI)
    {
        TreeSet treeElement = Array.Find(lines, (line) => line.talentUI == talentUI);

        if (treeElement.talentUI.talent.hasBought == false)
        {
            // Unlock
            if (treeElement.talentUI.talent.state == Talent.State.Unlock && playerContext.playerData.Money >= treeElement.talentUI.talent.Cost)
            {
                playerContext.playerData.Money -= treeElement.talentUI.talent.Cost;
                treeElement.talentUI.talent.hasBought = true;
                treeElement.talentUI.talent.state = Talent.State.Active;
            }
        }
        else
        {
            // Enable / Disable talent
            treeElement.UpdateTalentState();
        }

        RefreshTalentTree();
    }

    public void RefundTalents()
    {
        foreach (TreeSet line in lines)
        {
            if (line.talentUI.talent.hasBought)
            {
                line.talentUI.talent.hasBought = false;
                line.talentUI.talent.state = line.talentUI.talent.defaultRefundState;
                playerContext.playerData.Money += line.talentUI.talent.Cost / 2;
            }
        }

        RefreshTalentTree();
    }

    private void RefreshTalentTree()
    {
        foreach (TreeSet line in lines)
        {
            line.TryUnlockTalent();
            line.talentUI.UpdateToggle();
            playerContext.playerData.UpdateTalent(line.talentUI.talent, line.talentUI.talent.state == Talent.State.Active);
        }

        talentData.talents = lines.Select(line => line.talentUI.talent.GetSerializeData()).ToList();

        SaveSystem.SaveData(talentData, SaveSystem.Data.Talents);
        SaveSystem.SaveData(playerContext.playerData, SaveSystem.Data.PlayerData);
    }
}

