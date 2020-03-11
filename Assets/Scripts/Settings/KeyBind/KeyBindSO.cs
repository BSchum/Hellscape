using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputs", menuName = "ScriptableObjects/PlayerInputSO", order = 0)]
public class KeyBindSO : ScriptableObject
{
    public enum Key { Attack, Interact, MoveLeft, MoveRight, MoveForward, MoveBackward }

    public KeyCode attack;
    public KeyCode interact;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode moveForward;
    public KeyCode moveBackward;

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
}
