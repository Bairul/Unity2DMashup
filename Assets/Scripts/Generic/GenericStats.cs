using UnityEngine;

public abstract class GenericStats : MonoBehaviour
{
    [SerializeField] protected GenericScriptableObject genericStats;

    // iframe
    [SerializeField] private ImmunityFlash immunityFlash;
    [HideInInspector] public float invincibilityTimer;
    [HideInInspector] public bool isInvincible;

    // current stats
    [HideInInspector] public float currentMovementSpeed;
    [HideInInspector] public float currentDamage;

    public float currentHealth;

    protected virtual void Awake()
    {
        currentMovementSpeed = genericStats.MovementSpeed;
        currentDamage = genericStats.Damage;
        currentHealth = genericStats.MaxHealth;
        invincibilityTimer = genericStats.IFrameDuration;
        isInvincible = false;
    }

    public void CheckIFrame()
    {
        if (!isInvincible) return;

        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }
    }

    public void TakeDamage(float damage) 
    {
        if (isInvincible) return;
        
        DamageTaken(damage);

        if (currentHealth <= 0) 
        {
            Kill();
        }
        else
        {
            invincibilityTimer = genericStats.IFrameDuration;
            isInvincible = true;
            immunityFlash.Flash(genericStats.IFrameDuration);
        }
    }

    protected virtual void DamageTaken(float damage)
    {
        currentHealth -= damage;
    }

    protected abstract void Kill();
}
