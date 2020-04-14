using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonUI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Talent", LoadSceneMode.Single);
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
