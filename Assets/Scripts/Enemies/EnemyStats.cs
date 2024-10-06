using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject baseData;

    // Current Stats
    [HideInInspector]
    public float currentMovementSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
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
