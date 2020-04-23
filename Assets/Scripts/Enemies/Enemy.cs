using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDG.Unity.Scripts;

public class Enemy : MonoBehaviour, IDamagable
{
    public float health = 100;
    public float damageMultiplier = 1f;
    public int roomNumber;
    public SimpleRoom room;
    public int moneyReward;
    public PlayerContext playerContext;
    public Animator _animator;


    public virtual void TakeDamage(uint amount)
    {
        if (health > 0)
        {
            health -= amount * damageMultiplier;
            Debug.Log($"{this.name} a subit {amount * damageMultiplier}, il lui reste {health} PV");

            if (health <= 0)
            {
                playerContext.playerData.AddMoney(moneyReward);
                playerContext.goldEarned += moneyReward;
                StopAllCoroutines();
                _animator.SetTrigger("Die");
                Destroy(this.gameObject, 2f);
            }
        }
    }
}
