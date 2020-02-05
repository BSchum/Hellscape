using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[Serializable]
public class BaseStats
{
    public float Power;
    public float Agility;
    public float Health;
    public float MaxHealth;


    public static BaseStats operator +(BaseStats a, BaseStats b)
    {
        return new BaseStats()
        {
            Power = a.Power + b.Power,
            Agility = a.Agility + b.Agility,
            MaxHealth = a.MaxHealth + b.MaxHealth,
            Health = a.Health + b.Health
        };
    }
}

