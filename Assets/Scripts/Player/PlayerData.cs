using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSavedData", menuName = "Player/PlayerSavedData")]
public class PlayerData : ScriptableObject
{
    public List<Talent> activeTalents;
    public int Money;

    public void UpdateTalent(Talent talent, bool addTalent)
    {
        if (addTalent && !activeTalents.Contains(talent))
        {
            activeTalents.Add(talent);
        }
        else if (!addTalent)
        {
            activeTalents.Remove(talent);
        }
    }
}

