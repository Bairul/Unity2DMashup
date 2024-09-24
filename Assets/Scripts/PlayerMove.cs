using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rgbd2d;
    public Animate animate;
    public float mvtSpd;
    private Vector2 mvt;
    

    // Update is called once per frame
    void Update()
    {
        mvt.x = Input.GetAxisRaw("Horizontal");
        mvt.y = Input.GetAxisRaw("Vertical");
        
        animate.horizontal = mvt.x;
        mvt.Normalize();
        
        rgbd2d.velocity = mvt * mvtSpd;
    }
}
