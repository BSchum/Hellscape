using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

[Serializable]
public class TalentData : ISerializable
{
    public List<string> talents;

    // You need an empty constructor to deserialize
    public TalentData() { }

    public TalentData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
            throw new ArgumentNullException("info");

        string talentsJson = (string)info.GetValue("talents", typeof(string));

        talents = JsonUtility.FromJson<List<string>>(talentsJson);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        string activateTalentsJson = JsonUtility.ToJson(talents);

        info.AddValue("talents", (string)activateTalentsJson);
    }
}
