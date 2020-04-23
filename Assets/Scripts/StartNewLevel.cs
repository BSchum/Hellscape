using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewLevel : MonoBehaviour
{
    [SerializeField]
    PlayerContext playerContext;
    public void SetUpContext() {
        playerContext.currentLevel = 0;
        playerContext.goldEarned = 0;

        SceneManager.LoadScene("LevelScene");
    }
}
