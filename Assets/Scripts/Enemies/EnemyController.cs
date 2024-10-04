using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyAnimate animate;

    [SerializeField]
    private Rigidbody2D rgbd2d;

    [SerializeField]
    private EnemyStats stats;

    private Vector2 mvt;
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
        if (!animate.attack)
        {
            rgbd2d.velocity = mvt.normalized * stats.currentMovementSpeed;
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
        // Debug.Log("Attacking");
    }
}
