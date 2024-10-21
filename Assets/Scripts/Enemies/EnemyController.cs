using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyStats))]

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyAnimate animate;

    private Rigidbody2D rgbd2d;
    private EnemyStats stats;
    private Transform player;

    private Vector2 mvt;
    private bool canAttack;

    void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();
    }

    void Start() {
        player = GameWorld.Instance.GetPlayerTransform;
        canAttack = true;
    }

    void Update() {
        mvt = player.position - transform.position;
        animate.horizontal = (int) mvt.x;
        stats.CheckIFrame();
        
        GameWorld.Instance.UpdateNearestEnemy(gameObject);
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

        yield return new WaitForSeconds(stats.BaseStats.AttackCooldown);
        canAttack = true;
    }
}
