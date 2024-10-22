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
    private PlayerController playerController;
    private MouseIndicator mouseIndicator;

    protected override void Awake()
    {
        base.Awake();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        playerController = GetComponentInParent<PlayerController>();
        mouseIndicator = playerController.PlayerMouseIndicator;
    }

    public override IEnumerator Dash()
    {
        playerController.canMove = false;

        Vector3 toMouse = new(mouseIndicator.mouseDir.x, mouseIndicator.mouseDir.y);
        playerController.PlayerRigidBody.velocity = dashSpeedMultiplier * playerController.PlayerMovementSpeed * toMouse.normalized;

        // lastMvt.x = animate.mouseRight ? 1.0f : -1.0f;
        // lastMvt.y = 0;

        particleSystem.transform.rotation = mouseIndicator.rotationToMouse; 
        particleSystem.Play();

        yield return new WaitForSeconds(dashDuration);
        playerController.canMove = true;
        particleSystem.Stop();

        yield return new WaitForSeconds(dashCooldown);
        dashController.canDash = true;
    }
}
