using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.Tags.PLAYER_TAG)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Boss", LoadSceneMode.Single);
        }
    }
}
