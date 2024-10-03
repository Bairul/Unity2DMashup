using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject prefab;
    public float speed;
    public float cooldownDuration;
    public float currentCooldown;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentCooldown = cooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual private void Attack()
    {
        currentCooldown = cooldownDuration;
    }
}
