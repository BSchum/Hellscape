using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulOrb : MonoBehaviour
{
    public Soul soul;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.PLAYER_TAG)
        {
            other.GetComponent<Player>().Sword.IntegrateSoul(soul);
            Destroy(gameObject);
        }
    }
}
