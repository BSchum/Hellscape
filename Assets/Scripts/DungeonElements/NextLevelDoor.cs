using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == Constants.Tags.PLAYER_TAG)
        {
            SceneManager.LoadScene("LevelScene");
        }
    }
}
