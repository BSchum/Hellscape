using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public GameObject bossAnchor;
    public Boss chosenBoss;
    // Start is called before the first frame update
    void Start()
    {
        chosenBoss = BossesProvider.Instance.GetRandomBosses();
        Instantiate(chosenBoss, bossAnchor.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
