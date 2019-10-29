using UnityEngine;
public abstract class Buff
{

    protected GameObject Target { get; set;}
    public Buff(GameObject target)
    {
        Target = target;
    }
    public abstract void ApplyBuff();
}

