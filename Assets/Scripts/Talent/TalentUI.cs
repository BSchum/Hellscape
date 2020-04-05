using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TalentUI : MonoBehaviour
{
    public Talent talent;
    public Text talentNameUI, talentCostUI;
    public Image talentState;

    private Toggle toggle;
    [HideInInspector] public TalentTreeController talentTreeController;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.targetGraphic.gameObject.GetComponent<Image>().sprite = talent.sprite;
        toggle.onValueChanged.AddListener(OnClick);

        talentNameUI.text = talent.TalentName;
        talentCostUI.text = talent.Cost.ToString();
    }

    public void UpdateToggle()
    {
        talentCostUI.gameObject.SetActive(!talent.hasBought);

        toggle.interactable = talent.state != Talent.State.Lock;

        talentState.gameObject.SetActive(toggle.interactable);

        switch (talent.state)
        {
            case Talent.State.Active:
                talentState.color = Color.green;
                break;
            case Talent.State.Unlock:
                talentState.color = Color.yellow;
                break;
        }
    }

    public void OnClick(bool status)
    {
        talentTreeController.OnClickTalent(this);
    }
}
