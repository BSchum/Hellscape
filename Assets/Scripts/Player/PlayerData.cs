using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public class PlayerData : ISerializable
{
    [NonSerialized]  public List<Talent> activeTalents = new List<Talent>(); // this do not persist
    public int Money;

    public void AddMoney(int amount)
    {
        Money += amount;
        SaveSystem.SaveData(this, SaveSystem.Data.PlayerData);
    }

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

    // You need an empty constructor to deserialize
    public PlayerData() { }

    public PlayerData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
            throw new ArgumentNullException("info");

        Money = (int)info.GetValue("Money", typeof(int));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Money", (int)Money);
    }
}

