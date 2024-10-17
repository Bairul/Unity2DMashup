using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerStats))]

public class PlayerController : MonoBehaviour
{
    // unchanging fields
    private static Vector3 LEFT = new(-1f, 1f, 1f);
    private static Vector3 RIGHT = new(1f, 1f, 1f);

    // inspector fields
    [SerializeField] private PlayerAnimate animate;
    [SerializeField] private Transform playerHitbox;
    [SerializeField] private Transform playerFeetbox;
    [SerializeField] private new ParticleSystem particleSystem;
    public MouseIndicator mouseIndicator;

    // private fields
    private PlayerStats stats;
    private Rigidbody2D rgbd2d;
    private Vector2 mvt;
    private Vector2 lastMvt;
    private bool canDash;
    private bool isDashing;

    // Getters
    public Vector2 LastMovementDirection { get => lastMvt; }

    // Awake is called before Start. Frequently used to get internal components and initialize fields
    void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();

        canDash = true;
        isDashing = false;
        lastMvt = new(1f, 0);
    }

    // Used when referencing other objects and their components or after Awake
    void Start()
    {
        
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

        if (mvt.x != 0 || mvt.y != 0)
        {
            lastMvt.x = mvt.x;
            lastMvt.y = mvt.y;
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
        rgbd2d.velocity = stats.BaseStats.DashSpeedMultiplier * stats.currentMovementSpeed * toMouse.normalized;

        lastMvt.x = animate.mouseRight ? 1.0f : -1.0f;
        lastMvt.y = 0;

        particleSystem.transform.rotation = mouseIndicator.rotationToMouse; 
        particleSystem.Play();

        yield return new WaitForSeconds(stats.BaseStats.DashDuration);
        isDashing = false;
        particleSystem.Stop();

        yield return new WaitForSeconds(stats.BaseStats.DashCooldown);
        canDash = true;
    }

    // Update is called once per frame
    void Update() {
        PlayerMovement();
        CheckDash();
        stats.CheckIFrame();
    }
    
    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        if (isDashing) return;

        rgbd2d.velocity = mvt.normalized * stats.currentMovementSpeed;
    }
}
