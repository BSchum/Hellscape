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

    [System.Serializable]
    public struct LinkTree
    {
        public TalentUI talentUI;
        public Image link;
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

        if (requiredTalents.Count > 0)
        {
            var allRequiredTalentsBought = requiredTalents.All(link => { return link.talentUI.talent.hasBought && (link.talentUI.talent.state == Talent.State.Active || link.talentUI.talent.state == Talent.State.Unlock); });
            if(talentUI.talent.state != Talent.State.Active)
            {
                if (allRequiredTalentsBought)
                {
                    talentUI.talent.state = Talent.State.Unlock;
                }
                else
                {
                    talentUI.talent.state = Talent.State.Lock;
                }
            }


            foreach (LinkTree linkTree in requiredTalents)
            {
                if (linkTree.talentUI.talent.hasBought && (linkTree.talentUI.talent.state == Talent.State.Active || linkTree.talentUI.talent.state == Talent.State.Unlock))
                {
                    //Play the animation
                    linkTree.link.GetComponent<Animator>().SetBool("IsActive", true);
                }
                else
                {
                    //Play the animation reverse
                    linkTree.link.GetComponent<Animator>().SetBool("IsActive", false);

                }
            }
        }
    }
}
