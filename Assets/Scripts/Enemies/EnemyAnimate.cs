using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    private Animator animator;

    [HideInInspector]
    public int horizontal;
    
    [HideInInspector]
    public bool attack;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("Attack", attack);
        animator.SetInteger("Horizontal", horizontal);
    }

    void FinishAttackAnimation() {
        attack = false;
    }
}
