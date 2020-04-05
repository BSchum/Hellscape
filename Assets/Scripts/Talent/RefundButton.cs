using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefundButton : MonoBehaviour
{
    public TalentTreeController talentTreeController;
    public TalentUI talentUI;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        talentTreeController.RefundTalent(talentUI);
    }
}
