using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System;

public static class SaveSystem
{
    public enum Data { GraphicSetting, Inputs, Sounds, PlayerData, Talents }

    public static void SaveData(ISerializable o, Data data)
    {
        var jsonObject = JsonUtility.ToJson(o);

        string path = GetPath(data);

        File.WriteAllText(path, jsonObject);
    }

    public static T LoadData<T>(Data data)
    {
        string path = GetPath(data);
        T obj = default;

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);

            obj = JsonUtility.FromJson<T>(json);
        }
        else
        {
            obj = (T)Activator.CreateInstance(typeof(T));
            ISerializable t = (ISerializable)obj;

            SaveData(t, data);
        }

        return obj;
    }

    private static string GetPath(Data data)
    {
        string path = Application.persistentDataPath + "/" + data.ToString() + ".data";
        
        return path;
    }
}
