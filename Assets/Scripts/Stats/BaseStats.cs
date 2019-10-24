using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class BaseStats
{
    public float Power { get; set; }
    public float Agility { get; set; }
    public float Health { 
        get {
            return _health;
        }
        set { 
            if(_health + value <= MaxHealth)
            {
                _health += value;
            }
            else
            {
                _health = MaxHealth;
            }
        } 
    }
    public float MaxHealth { get; set; }

    private float _health { get; set; }

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

