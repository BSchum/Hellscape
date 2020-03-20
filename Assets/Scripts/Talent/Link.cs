using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Link : MonoBehaviour
{
    [SerializeField] TalentUI sourceTalent;
    [SerializeField] TalentUI destinationTalent;
    [SerializeField] List<RawImage> images;
    private bool _isActive = false;
    public bool IsActive { 
        get {
            return _isActive;
        } 
    }

    public void ActivateLink()
    {
        Debug.Log("Je m'active! " + gameObject.name);
        _isActive = true;
        ChangeState();
        destinationTalent.TryMakeInteractable();
    }

    public void DesactivateLink()
    {
        Debug.Log("Je me desactive! " + gameObject.name);
        _isActive = false;
        ChangeState();
        destinationTalent.TryMakeInteractable();
    }

    private void ChangeState()
    {
        if (_isActive)
        {
            foreach(RawImage images in images)
            {
                images.color = Color.green;
            }
        }
        else
        {
            foreach (RawImage images in images)
            {
                images.color = Color.white;
            }
        }
    }

    public bool IsDestTalentOn()
    {
        return destinationTalent.GetComponent<Toggle>().isOn;
    }
}
