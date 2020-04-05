using UnityEngine;

[CreateAssetMenu(fileName = "Talent", menuName = "Player/Talent")]
public class Talent : ScriptableObject
{
    public enum State { Lock, Unlock, Active }; // Bought is also for Disable
    public State state = State.Lock;
    public State defaultRefundState = State.Lock;

    public bool hasBought = false;

    public string TalentName;
    public Stats stats;
    public int Cost = 10;

    public Sprite sprite;
}
