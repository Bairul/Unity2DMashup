using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    public Animator animator;
    public int horizontal;
    public int vertical;
    public bool mouseRight;

    void Update()
    {
        animator.SetInteger("Vertical", vertical);
        animator.SetInteger("Horizontal", horizontal);
        animator.SetBool("MouseRight", mouseRight);
    }
}
