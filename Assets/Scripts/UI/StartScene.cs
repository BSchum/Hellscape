using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var managers = GameObject.Find("__GlobalsManager__");
        if(managers != null)
            Destroy(managers);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelScene");
    }
}
