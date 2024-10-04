using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    public Animator animator;
    public int horizontal;
    public bool attack;

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Attack", attack);
        animator.SetInteger("Horizontal", horizontal);
    }
}
