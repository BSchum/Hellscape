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
        state = !state;

        animator.SetBool("Display", state);
        animator.SetTrigger("OnArrowClick");
    }
}
