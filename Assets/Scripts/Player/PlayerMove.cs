using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{
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
    public float mvtSpd;
    private Vector2 mvt;

    // Update is called once per frame
    void Update() {
        mvt.x = Input.GetAxisRaw("Horizontal");
        mvt.y = Input.GetAxisRaw("Vertical");
        
        animate.horizontal = (int) mvt.x;
        animate.vertical = (int) mvt.y;

        animate.mouseRight = mouseIndicator.mousePos.x - transform.position.x >= 0;

        // if (animate.horizontal < 0) {
        //     playerHitbox.localScale = new Vector3(-1f, 1f, 1f);
        //     playerFeetbox.localScale = new Vector3(-1f, 1f, 1f);
        // } else if (animate.horizontal > 0) {
        //     playerHitbox.localScale = new Vector3(1f, 1f, 1f);
        //     playerFeetbox.localScale = new Vector3(1f, 1f, 1f);
        // }
    }
    
    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        mvt.Normalize();
        rgbd2d.velocity = mvt * mvtSpd;
    }
}
