using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Soul : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public uint powerBonus;
    public float attackSpeedBonus;
    public string animatorAttackToggle;
}
