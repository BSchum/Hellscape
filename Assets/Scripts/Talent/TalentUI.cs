using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TalentUI : MonoBehaviour
{
    public Talent talent;
    public Text talentCostUI;
    public Image talentCostIcon;
    public Image talentState;

    private Toggle toggle;
    [HideInInspector] public TalentTreeController talentTreeController;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnClick);
        talentCostUI.text = talent.Cost.ToString();
    }

    public void UpdateToggle()
    {
        talentCostUI.gameObject.SetActive(!talent.hasBought);
        talentCostIcon.gameObject.SetActive(!talent.hasBought);

        toggle.interactable = talent.state != Talent.State.Lock;
        switch (talent.state)
        {
            case Talent.State.Active:
                talentState.sprite = talent.ActiveSprite;
                break;
            case Talent.State.Unlock:
                if (talent.hasBought)
                    talentState.sprite = talent.BoughtUnactiveSprite;
                else if (!talent.hasBought)
                    talentState.sprite = talent.NonBoughtUnactiveSprite;
                break;
            default:
                talentState.sprite = talent.LockedSprite;
                break;
        }
    }

    public void OnClick(bool status)
    {
        talentTreeController.OnClickTalent(this);
    }
}
