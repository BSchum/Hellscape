using UnityEngine;

namespace SDG.Scripts.Room
{
    public class SimpleRoom : MonoBehaviour
    {
        public GameObject enemyHolder;
        public GameObject enemyPrefab;
        public void Start()
        {
            Instantiate(enemyPrefab, enemyHolder.transform.position, Quaternion.identity ,enemyHolder.transform);
        }
    }
}
