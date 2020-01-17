using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    public uint damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Constants.Tags.PLAYER_TAG)
        {
            Debug.Log("J'inflige des dégats au joueur");
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
