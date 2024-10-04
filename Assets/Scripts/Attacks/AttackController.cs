using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    protected AttackStats attackStats;

    [SerializeField]
    protected PlayerStats playerStats;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (attackStats.CanAttack())
        {
            LaunchAttack();
        }
    }

    protected virtual private void LaunchAttack()
    {

    }
}
