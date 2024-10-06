using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    public Animator animator;
    public int horizontal;
    public bool attack;

    void Update()
    {
        animator.SetBool("Attack", attack);
        animator.SetInteger("Horizontal", horizontal);
    }

    void FinishAttackAnimation() {
        attack = false;
    }
}
