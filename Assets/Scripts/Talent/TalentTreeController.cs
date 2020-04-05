using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TalentTreeController : MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField] private TreeSet[] lines;

    void Start()
    {
        foreach (TreeSet line in lines)
        {
            line.talentUI.talentTreeController = this;
        }

        RefreshTalentTree();
    }

    public void OnClickTalent(TalentUI talentUI)
    {
        TreeSet treeElement = Array.Find(lines, (line) => line.talentUI == talentUI);

        if (treeElement.talentUI.talent.hasBought == false)
        {
            // Unlock
            if (treeElement.talentUI.talent.state == Talent.State.Unlock && playerData.Money >= treeElement.talentUI.talent.Cost)
            {
                playerData.Money -= treeElement.talentUI.talent.Cost;
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

    public void RefundTalent(TalentUI talentUI)
    {
        if (talentUI.talent.hasBought)
        {
            talentUI.talent.hasBought = false;
            talentUI.talent.state = talentUI.talent.defaultRefundState;
            playerData.Money += talentUI.talent.Cost / 2;
        }

        RefreshTalentTree();
    }

    private void RefreshTalentTree()
    {
        foreach (TreeSet line in lines)
        {
            line.TryUnlockTalent();
            line.talentUI.UpdateToggle();
            playerData.UpdateTalent(line.talentUI.talent, line.talentUI.talent.state == Talent.State.Active);
        }
    }
}

