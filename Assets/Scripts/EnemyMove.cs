using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Animate animate;
    [SerializeField]
    private Rigidbody2D rgbd2d;
    public float mvtSpd = 2f; // Movement speed of the enemy
    private Transform player; // Reference to the player

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        // Calculate direction to the player
        Vector2 direction = player.position - transform.position;

        animate.horizontal = (int) direction.x;
        direction.Normalize();

        rgbd2d.velocity = direction * mvtSpd;
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
