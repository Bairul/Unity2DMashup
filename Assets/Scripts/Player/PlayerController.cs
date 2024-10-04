using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    private static Vector3 LEFT = new(-1f, 1f, 1f);
    private static Vector3 RIGHT = new(1f, 1f, 1f);

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

    [SerializeField]
    private ParticleSystem particleSystem;

    private Vector2 mvt;
    private bool canDash;
    private bool isDashing;

    void Start()
    {
        canDash = true;
        isDashing = false;
    }

    void PlayerMovement()
    {
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

    void CheckDash()
    {
        if (canDash && Input.GetButton("Dash"))
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector3 toMouse = new(mouseIndicator.mouseDir.x, mouseIndicator.mouseDir.y);
        rgbd2d.velocity = playerStats.baseData.DashSpeed * playerStats.currentMovementSpeed * toMouse.normalized;

        float rotZ =  Mathf.Atan2(mouseIndicator.mouseDir.y, mouseIndicator.mouseDir.x) * Mathf.Rad2Deg;
        particleSystem.transform.rotation = Quaternion.Euler(0, 0, rotZ); 
        particleSystem.Play();

        yield return new WaitForSeconds(playerStats.baseData.DashDuration);
        isDashing = false;
        particleSystem.Stop();

        yield return new WaitForSeconds(playerStats.baseData.DashCooldown);
        canDash = true;
    }

    // Update is called once per frame
    void Update() {
        PlayerMovement();
        CheckDash();
    }
    
    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        if (isDashing) return;

        rgbd2d.velocity = mvt.normalized * playerStats.currentMovementSpeed;
    }
}
