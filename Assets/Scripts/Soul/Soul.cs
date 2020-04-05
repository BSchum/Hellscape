using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Soul : MonoBehaviour
{
    public string itemName;
    public Sprite sprite;
    public uint powerBonus;
    public float attackSpeedBonus;
    public string animatorAttackToggle;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.PLAYER_TAG)
        {
            other.GetComponent<Player>().Sword.IntegrateSoul(this);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
