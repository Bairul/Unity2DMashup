using System.Collections;
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
    private Transform player; // Reference to the player

    private Vector2 mvt;
    
    private bool canAttack;

    void Start() {
        // TODO: change me in the future to something efficient
        player = GameObject.FindGameObjectWithTag("Player").transform;
        canAttack = true;
    }

    void Update() {
        mvt = player.position - transform.position;
        animate.horizontal = (int) mvt.x;
        stats.CheckIFrame();
    }

    void FixedUpdate()
    {
        if (!animate.attack) // Don't move when playing attack animation
        {
            rgbd2d.velocity = mvt.normalized * stats.currentMovementSpeed;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (canAttack && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Attack(collision.gameObject.GetComponent<PlayerStats>()));
        }
    }

    private IEnumerator Attack(PlayerStats player)
    {
        animate.attack = true;
        canAttack = false;
        player.TakeDamage(stats.currentDamage);

        yield return new WaitForSeconds(stats.BaseData.AttackCooldown);
        canAttack = true;
    }
}
