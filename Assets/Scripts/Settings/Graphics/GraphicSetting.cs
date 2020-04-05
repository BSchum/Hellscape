using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[Serializable]
public class GraphicSetting : ISerializable
{
    public int graphicLevel;
    public int screenMode;
    public int resolution;
    public bool isFullScreen;
    public int shadow;
    public int antiAliasing;
    public int texture;

    // You need an empty constructor to deserialize
    public GraphicSetting() { }

    public GraphicSetting(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
            throw new ArgumentNullException("info");

        graphicLevel = (int)info.GetValue("graphicLevel", typeof(int));
        screenMode = (int)info.GetValue("screenMode", typeof(int));
        resolution = (int)info.GetValue("resolution", typeof(int));
        isFullScreen = (bool)info.GetValue("isFullScreen", typeof(bool));
        shadow = (int)info.GetValue("shadow", typeof(int));
        antiAliasing = (int)info.GetValue("antiAliasing", typeof(int));
        texture = (int)info.GetValue("texture", typeof(int));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("graphicLevel", graphicLevel);
        info.AddValue("screenMode", screenMode);
        info.AddValue("resolution", resolution);
        info.AddValue("isFullScreen", isFullScreen);
        info.AddValue("shadow", shadow);
        info.AddValue("antiAliasing", antiAliasing);
        info.AddValue("texture", texture);
    }
}
