using System;
using UnityEngine;
[Serializable]
public class Stats
{
    /// <summary>
    /// Amount of damage dealed by one hit. (must be positive)
    /// </summary>
    public uint Power;
    public int Speed;
    public uint Health;
    public uint MaxHealth;

    public static Stats operator +(Stats a, Stats b)
    {
        return new Stats()
        {
            Power = a.Power + b.Power,
            Speed = a.Speed + b.Speed,
            MaxHealth = a.MaxHealth + b.MaxHealth,
            Health = a.Health + b.Health
        };
    }

    public void TakeDamage(uint amount)
    {
        if(amount > Health)
        {
            Health = 0;
        }
        else
        {
            Health -= amount;
        }
    }
}
