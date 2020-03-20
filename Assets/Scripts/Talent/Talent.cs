using UnityEngine;

[CreateAssetMenu(fileName = "Talent", menuName = "Player/Talent")]
public class Talent : ScriptableObject
{
    public string TalentName;
    public Stats stats;
    public int Cost;
}
