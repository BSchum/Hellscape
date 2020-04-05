using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class SoundData : ISerializable
{
    public float master;
    public float ambient;
    public float fx;

    // You need an empty constructor to deserialize
    public SoundData() { }

    public SoundData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
            throw new ArgumentNullException("info");

        master = (float)info.GetValue("master", typeof(float));
        ambient = (float)info.GetValue("ambient", typeof(float));
        fx = (float)info.GetValue("fx", typeof(float));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("attack", (float)(master));
        info.AddValue("interact", (float)ambient);
        info.AddValue("moveBackward", (float)fx);
    }
}