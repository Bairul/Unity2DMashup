using UnityEngine;

public class ExperienceOrb : MonoBehaviour, ICollectible
{
    public int experienceGranted;
    public float pullSpeed;

    public void Collect(PlayerStats player)
    {
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }

    public void MoveTowards(Transform otherTransform)
    {
        Vector2 direction = (otherTransform.position - transform.position).normalized;
        Vector3 vel = pullSpeed * Time.deltaTime * direction;
        transform.position += vel;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Hitbox"))
        {
            Collect(collider2D.gameObject.GetComponentInParent<PlayerStats>());
        }
    }
}
