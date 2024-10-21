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
    [SerializeField] private MouseIndicator mouseIndicator;

    // private fields
    private PlayerStats stats;
    private Rigidbody2D rgbd2d;
    private Vector2 mvt;
    private Vector2 lastMvt;

    public bool canMove;

    // Getters
    public Vector2 LastMovementDirection { get => lastMvt; }
    public MouseIndicator PlayerMouseIndicator { get => mouseIndicator; }
    public Rigidbody2D PlayerRigidBody { get => rgbd2d; }
    public float PlayerMovementSpeed { get => stats.currentMovementSpeed; }

    // Awake is called before Start. Frequently used to get internal components and initialize fields
    void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();

        lastMvt = new(1f, 0);
        canMove = true;
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

    // Update is called once per frame
    void Update() {
        PlayerMovement();
        stats.CheckIFrame();
    }
    
    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        if (!canMove) return;

        rgbd2d.velocity = mvt.normalized * stats.currentMovementSpeed;
    }
}
