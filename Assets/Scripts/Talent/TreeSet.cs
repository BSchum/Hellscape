using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class TreeSet
{
    public TalentUI talentUI;
    public List<LinkTree> requiredTalents;
    public List<LinkTree> optionalTalents;

    [System.Serializable]
    public struct LinkTree
    {
        public TalentUI talentUI;
        public List<RawImage> links;
    }

    public void UpdateTalentState()
    {
        if (talentUI.talent.hasBought && talentUI.talent.state == Talent.State.Active)
        {
            talentUI.talent.state = Talent.State.Unlock;
        }
        else if (talentUI.talent.hasBought && talentUI.talent.state == Talent.State.Unlock)
        {
            talentUI.talent.state = Talent.State.Active;
        }
    }

    public void TryUnlockTalent()
    {
        if (talentUI.talent.state == Talent.State.Active)
            return;

        if (requiredTalents.Count > 0)
        {
            var allRequiredTalentsBought = requiredTalents.All(link => { return link.talentUI.talent.hasBought && (link.talentUI.talent.state == Talent.State.Active || link.talentUI.talent.state == Talent.State.Unlock); });
            if (allRequiredTalentsBought)
            {
                talentUI.talent.state = Talent.State.Unlock;
            }
            else
            {
                talentUI.talent.state = Talent.State.Lock;
            }

            foreach (LinkTree linkTree in requiredTalents)
            {
                if (linkTree.talentUI.talent.hasBought && (linkTree.talentUI.talent.state == Talent.State.Active || linkTree.talentUI.talent.state == Talent.State.Unlock))
                {
                    foreach (RawImage link in linkTree.links)
                        link.color = Color.green;
                }
                else
                {
                    foreach (RawImage link in linkTree.links)
                        link.color = Color.white;
                }
            }
        }

        if (optionalTalents.Count > 0)
        {
            var oneOptionalTalentBought = optionalTalents.Exists(link => { return link.talentUI.talent.hasBought && (link.talentUI.talent.state == Talent.State.Active || link.talentUI.talent.state == Talent.State.Unlock); });
            if (oneOptionalTalentBought)
            {
                talentUI.talent.state = Talent.State.Unlock;
            }
            else
            {
                talentUI.talent.state = Talent.State.Lock;
            }

            foreach (LinkTree linkTree in optionalTalents)
            {
                if (linkTree.talentUI.talent.hasBought && (linkTree.talentUI.talent.state == Talent.State.Active || linkTree.talentUI.talent.state == Talent.State.Unlock))
                {
                    foreach (RawImage link in linkTree.links)
                        link.color = Color.green;
                }
                else
                {
                    foreach (RawImage link in linkTree.links)
                        link.color = Color.white;
                }
            }
        }
    }
}
