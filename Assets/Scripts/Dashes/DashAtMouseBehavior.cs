using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAtMouseBehavior : DashBehavior
{
    [SerializeField] 
    private float dashSpeedMultiplier;

    [SerializeField] 
    private float dashDuration;

    private new ParticleSystem particleSystem;
    private MouseIndicator mouseIndicator;
    private Rigidbody2D rgbd2d;

    protected override void Awake()
    {
        base.Awake();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        mouseIndicator = GameWorld.Instance.GetPlayerController.PlayerMouseIndicator;
        rgbd2d = GameWorld.Instance.GetPlayerController.PlayerRigidBody;
    }

    public override IEnumerator Dash()
    {
        GameWorld.Instance.GetPlayerController.canMove = false;

        Vector3 toMouse = new(mouseIndicator.mouseDir.x, mouseIndicator.mouseDir.y);
        rgbd2d.velocity = dashSpeedMultiplier * GameWorld.Instance.GetPlayerController.PlayerMovementSpeed * toMouse.normalized;

        // lastMvt.x = animate.mouseRight ? 1.0f : -1.0f;
        // lastMvt.y = 0;

        particleSystem.transform.rotation = mouseIndicator.rotationToMouse; 
        particleSystem.Play();

        yield return new WaitForSeconds(dashDuration);
        GameWorld.Instance.GetPlayerController.canMove = true;
        particleSystem.Stop();

        yield return new WaitForSeconds(dashCooldown);
        dashController.canDash = true;
    }
}
