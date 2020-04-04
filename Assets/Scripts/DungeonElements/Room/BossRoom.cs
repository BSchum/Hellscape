using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : DefaultRoom
{
    public GameObject bossAnchor;
    public GameObject playerAnchor;
    public GameObject playerPrefab;
    public Boss chosenBoss;
    public PlayerContext playerContext;
    public GameObject exit;
    GameObject _instantiatedBoss;


    // Start is called before the first frame update
    void Start()
    {

        PlayerUIManager.GetInstance().Awake();
        
        chosenBoss = BossesProvider.Instance.GetRandomBosses();
        _instantiatedBoss = Instantiate(chosenBoss, bossAnchor.transform.position, Quaternion.identity).gameObject;
        playerContext.player.transform.position = playerAnchor.transform.position;
        Camera.main.transform.position = cameraHolder.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_instantiatedBoss == null)
        {
            exit.SetActive(true);
        }

    }
}
