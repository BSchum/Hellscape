using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsArrow : MonoBehaviour
{
    [SerializeField]
    private bool state;
    public Animator animator;

    public void Display()
    {
        animator.SetBool("Display", state);

        state = !state;
    }
}
