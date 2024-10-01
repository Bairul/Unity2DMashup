using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMove : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Animate animate;
    public float mvtSpd = 2f; // Movement speed of the enemy
    public Rigidbody2D rgbd2d;

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector2 direction = player.position - transform.position;

            animate.horizontal = (int) direction.x;
            direction.Normalize();
            
            rgbd2d.velocity = direction * mvtSpd;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking");
    }
}
