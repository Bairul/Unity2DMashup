using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    private static Vector3 LEFT = new Vector3(-1f, 1f, 1f);
    private static Vector3 RIGHT = new Vector3(1f, 1f, 1f);

    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private Rigidbody2D rgbd2d;

    [SerializeField]
    private PlayerAnimate animate;

    [SerializeField]
    private Transform playerHitbox;

    [SerializeField]
    private Transform playerFeetbox;

    [SerializeField]
    private MouseIndicator mouseIndicator;

    private Vector2 mvt;

    // Update is called once per frame
    void Update() {
        mvt.x = Input.GetAxisRaw("Horizontal");
        mvt.y = Input.GetAxisRaw("Vertical");
        
        animate.horizontal = (int) mvt.x;
        animate.vertical = (int) mvt.y;

        animate.mouseRight = mouseIndicator.mousePos.x - transform.position.x >= 0;

        if (animate.horizontal < 0) {
            playerHitbox.localScale = LEFT;
            playerFeetbox.localScale = LEFT;
        } else if (animate.horizontal > 0) {
            playerHitbox.localScale = RIGHT;
            playerFeetbox.localScale = RIGHT;
        }
    }
    
    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        rgbd2d.velocity = mvt.normalized * playerStats.currentMovementSpeed;
    }
}
