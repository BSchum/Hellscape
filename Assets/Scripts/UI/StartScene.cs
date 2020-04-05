using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public string levelName = "LevelScene";

    void Start()
    {
        var managers = GameObject.Find("__GlobalsManager__");
        if(managers != null)
            Destroy(managers);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
