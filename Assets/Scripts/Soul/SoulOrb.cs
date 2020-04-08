using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoulOrb : MonoBehaviour
{
    public Soul soul;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Constants.Tags.PLAYER_TAG)
        {
            collision.gameObject.GetComponent<Player>().Sword.IntegrateSoul(soul);
            Destroy(gameObject);
            // TEMPORAIRE
            SceneManager.LoadScene("Talent");
        }
    }
}
