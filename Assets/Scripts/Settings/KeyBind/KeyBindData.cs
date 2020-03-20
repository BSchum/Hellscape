using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class KeyBindData : ISerializable
{
    public enum Key { Attack, Interact, MoveLeft, MoveRight, MoveForward, MoveBackward }

    public KeyCode attack = KeyCode.Mouse0;
    public KeyCode interact = KeyCode.F;
    public KeyCode moveLeft = KeyCode.Q;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveForward = KeyCode.Z;
    public KeyCode moveBackward = KeyCode.S;

    public void SetKey(Key key, KeyCode newKey)
    {
        switch(key)
        {
            case Key.Attack:
                attack = newKey;
                break;
            case Key.Interact:
                interact = newKey;
                break;
            case Key.MoveLeft:
                moveLeft = newKey;
                break;
            case Key.MoveRight:
                moveRight = newKey;
                break;
            case Key.MoveForward:
                moveForward = newKey;
                break;
            case Key.MoveBackward:
                moveBackward = newKey;
                break;

            default:
                break;
        }
    }

    // You need an empty constructor to deserialize
    public KeyBindData() { }

    public KeyBindData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
            throw new ArgumentNullException("info");

        attack = (KeyCode)info.GetValue("graphicLevel", typeof(int));
        interact = (KeyCode)info.GetValue("interact", typeof(int));
        moveLeft = (KeyCode)info.GetValue("moveLeft", typeof(int));
        moveRight = (KeyCode)info.GetValue("moveRight", typeof(int));
        moveForward = (KeyCode)info.GetValue("moveForward", typeof(int));
        moveBackward = (KeyCode)info.GetValue("moveBackward", typeof(int));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("attack", (int)(attack));
        info.AddValue("interact", (int)interact);
        info.AddValue("moveLeft", (int)moveLeft);
        info.AddValue("moveRight", (int)moveRight);
        info.AddValue("moveForward", (int)moveForward);
        info.AddValue("moveBackward", (int)moveBackward);
    }
}
