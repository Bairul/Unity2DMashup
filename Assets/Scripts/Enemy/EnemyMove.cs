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
    private EnemyAnimate animate;
    [SerializeField]
    private Rigidbody2D rgbd2d;
    private Vector2 mvt;
    public float mvtSpd = 2f; // Movement speed of the enemy
    private Transform player; // Reference to the player

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        mvt = player.position - transform.position;
        animate.horizontal = (int) mvt.x;
    }

    void FixedUpdate()
    {
        mvt.Normalize();

        if (!animate.attack)
        {
            rgbd2d.velocity = mvt * mvtSpd;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animate.attack = true;
            Attack();
        }
    }

    void OnCollisionExit2D()
    {
        animate.attack = false;
    }

    private void Attack()
    {
        Debug.Log("Attacking");
    }
}
