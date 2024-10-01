using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public Animator animator;
    public int horizontal;
    public int vertical;

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Vertical", vertical);
        animator.SetInteger("Horizontal", horizontal);
    }
}
