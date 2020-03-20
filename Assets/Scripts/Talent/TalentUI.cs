using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TalentUI : MonoBehaviour
{
    [SerializeField] Talent talent;
    [SerializeField] Sprite talentImage;
    [SerializeField] List<Link> sourceLinks;
    [SerializeField] List<Link> destLinks;
    [SerializeField] PlayerData playerData;

    private Toggle toggle;
    private void Start()
    {

        toggle = this.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnClick);

        foreach (var link in sourceLinks)
        {
            if (!link.IsActive)
            {
                toggle.interactable = false;
            }
        }
    }

    public void TryMakeInteractable()
    {
        var interactable = true;
        foreach (var link in sourceLinks)
        {
            if (!link.IsActive)
            {
                interactable = false;
            }
        }
        
        Debug.Log($"Est ce que je m'active? {interactable} {name}");
        toggle.interactable = interactable;
    }

    //Called by toggle
    public void OnClick(bool isOn)
    {
        Debug.Log("OnClick Toggle -> " + isOn);
        if(destLinks.Any(l => l.IsDestTalentOn()))
        {
            toggle.isOn = true;
            return;
        }

        if (playerData.Money < talent.Cost)
        {
            Debug.Log("Pas assez de money : " + playerData.Money);
            toggle.isOn = false;
            return;
        }

        if (isOn)
        {
            playerData.Money -= talent.Cost;
            playerData.activeTalents.Add(talent);
            foreach (var link in destLinks)
            {
                link.ActivateLink();
            }
        }
        else
        {
            playerData.Money += talent.Cost;
            playerData.activeTalents.Remove(talent);
            foreach (var link in destLinks)
            {
                link.DesactivateLink();
            }
        }
    }
}
