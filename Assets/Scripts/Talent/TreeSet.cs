using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class TreeSet
{
    public TalentUI talentUI;
    public List<TalentUI> requiredTalents;
    public List<TalentUI> optionalTalents;

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
            var allRequiredTalentsBought = requiredTalents.All(link => { return link.talent.hasBought && (link.talent.state == Talent.State.Active || link.talent.state == Talent.State.Unlock); });
            if (allRequiredTalentsBought)
            {
                talentUI.talent.state = Talent.State.Unlock;
            }
            else
            {
                talentUI.talent.state = Talent.State.Lock;
            }
        }

        if (optionalTalents.Count > 0)
        {
            var oneOptionalTalentBought = optionalTalents.Exists(link => { return link.talent.hasBought && (link.talent.state == Talent.State.Active || link.talent.state == Talent.State.Unlock); });
            if (oneOptionalTalentBought)
            {
                talentUI.talent.state = Talent.State.Unlock;
            }
            else
            {
                talentUI.talent.state = Talent.State.Lock;
            }
        }
    }
}
