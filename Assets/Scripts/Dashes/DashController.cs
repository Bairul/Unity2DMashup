using UnityEngine;

public class DashController : MonoBehaviour
{
    public bool canDash;
    private DashBehavior dashBehavior;

    void Awake()
    {
        dashBehavior = GetComponent<DashBehavior>();
        canDash = true;
    }

    void Update()
    {
        if (canDash && Input.GetButton("Dash"))
        {
            canDash = false;
            StartCoroutine(dashBehavior.Dash());
        }
    }
}
