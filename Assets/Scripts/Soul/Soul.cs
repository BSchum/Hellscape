using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Soul : MonoBehaviour
{
    public string itemName;
    public Sprite sprite;
    public Stats bonusStats;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.PLAYER_TAG)
        {
            other.GetComponent<Player>().Sword.IntegrateSoul(this);
        }
    }
}
