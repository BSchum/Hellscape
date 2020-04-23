using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public string levelName = "LevelScene";

    void Start()
    {

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
