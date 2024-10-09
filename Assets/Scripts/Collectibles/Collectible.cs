using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public float pullSpeed;

    protected abstract void Collect(PlayerStats player);

    public void MoveTowards(Transform other)
    {
        Vector2 direction = (other.position - transform.position).normalized;
        Vector3 vel = pullSpeed * Time.deltaTime * direction;
        transform.position += vel;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("PlayerHitbox"))
        {
            Collect(collider2D.gameObject.GetComponentInParent<PlayerStats>());
        }
    }
}
