using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaine : MonoBehaviour
{
    public HingeJoint lastChainJoin;

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
                hellDogoRb.GetComponent<HellDoggy>().isChained = false;
                hellDogoRb.GetComponent<Animator>().SetBool("IsChained", false);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.ENEMY_TAG)
        {
            
            lastChainJoin.connectedBody = other.attachedRigidbody;
            hellDogoRb = other.attachedRigidbody;
            if (hellDogoRb.GetComponent<HellDoggy>().isChained != true) { 
                hellDogoRb.GetComponent<HellDoggy>().isChained = true;
                hellDogoRb.GetComponent<Animator>().SetBool("IsChained", true);
            }
        }
    }
}
