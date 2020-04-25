using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDG.Unity.Scripts;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable
{
    public float health = 100;
    public float damageTakenMultiplier = 1f;
    public int damageMultiplier = 1;
    public int roomNumber;
    public SimpleRoom room;
    public int moneyReward;
    public PlayerContext playerContext;
    public Animator _animator;

    void Awake() {
        Debug.Log("dmul :" + Mathf.Ceil((float)playerContext.currentLevel / 2));
        damageMultiplier = (int)Mathf.Ceil((float)playerContext.currentLevel / 2);//Le multiplicateur augmente de 1 tout les 4 niveaux
        health *= damageMultiplier;
    }
    public virtual void TakeDamage(uint amount)
    {
        if (health > 0)
        {
            health -= amount * damageTakenMultiplier;

            Debug.Log($"{this.name} a subit {amount * damageTakenMultiplier}, il lui reste {health} PV");
            PlayerUIManager.instance.CreateFloatingText((amount * damageTakenMultiplier).ToString(), this.transform);
            if (health <= 0)
            {
                playerContext.EarnGold(this.moneyReward);
                StopAllCoroutines();
                _animator.SetTrigger("Die");
                Destroy(this.gameObject, 2f);
            }
        }
    }
}
