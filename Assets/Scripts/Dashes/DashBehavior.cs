using System.Collections;
using UnityEngine;

public abstract class DashBehavior : MonoBehaviour
{
    [SerializeField] 
    protected float dashCooldown;
    
    protected DashController dashController;
    protected PlayerStats playerStats;

    protected virtual void Awake()
    {
        dashController = GetComponent<DashController>();
        playerStats = GetComponentInParent<PlayerStats>();
    }

    public abstract IEnumerator Dash();
}
