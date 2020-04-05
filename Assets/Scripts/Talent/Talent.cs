using System;
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

    /*
     * Beacuse i can't serialize SO, i'll get a pre serialize data from the SO and then save only that data
     * Then i'll load that data and change my talents using that data
     */
    private struct TalentData
    {
        public State state;
        public bool hasBought;
    }

    public string GetSerializeData()
    {
        TalentData talentData = new TalentData
        {
            state = state,
            hasBought = hasBought
        };

        return JsonUtility.ToJson(talentData);
    }

    public void SetDataFromSerialize(string data)
    {
        TalentData talentData = JsonUtility.FromJson<TalentData>(data);
        state = talentData.state;
        hasBought = talentData.hasBought;
    }
}
