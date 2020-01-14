using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaine : MonoBehaviour
{
    public HingeJoint lastChaineJoin;

    private Rigidbody hellDogoRb = null;
    public MeshRenderer[] chainMesh;
    private float chainLife = 0;
    public float chainLifetime = 3f;
    public Color brokenChain;
    public Color baseChainColor;

    private void Update()
    {
        if (hellDogoRb != null)
        {
            chainLife += 0.1f * Time.deltaTime;
            foreach (MeshRenderer m in chainMesh)
                m.material.color = Color.Lerp(m.material.color, brokenChain, chainLife);
        }

        if (chainLife >= chainLifetime)
        {
            if (hellDogoRb != null)
            {
                hellDogoRb.GetComponent<HellDoggo>().isChained = false;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.ENEMY_TAG)
        {
            lastChaineJoin.connectedBody = other.attachedRigidbody;
            hellDogoRb = other.attachedRigidbody;
            hellDogoRb.GetComponent<HellDoggo>().isChained = true;
        }
    }
}
