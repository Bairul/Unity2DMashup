using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    public Animator animator;
    public int horizontal;
    public int vertical;
    public bool mouseRight;

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Vertical", vertical);
        animator.SetInteger("Horizontal", horizontal);
        animator.SetBool("MouseRight", mouseRight);
    }
}
