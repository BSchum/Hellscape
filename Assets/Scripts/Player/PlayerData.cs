using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSavedData", menuName = "Player/PlayerSavedData")]
public class PlayerData : ScriptableObject
{
    public List<Talent> activeTalents;
    public int Money;
}

