using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject baseData;

    // Current Stats
    public float currentMovementSpeed;
    public float currentHealth;
    public float currentDamage;

    void Awake()
    {
        currentMovementSpeed = baseData.MovementSpeed;
        currentHealth = baseData.MaxHealth;
        currentDamage = baseData.Damage;
    }

    public void TakeDamage(float damage) 
    {
        currentHealth -= damage;

        if (currentHealth <= 0) 
        {
            Kill();
        }
    }

    public void Kill() 
    {
        Destroy(gameObject);
    }
}
