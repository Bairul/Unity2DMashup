using UnityEngine;

public abstract class GenericStats : MonoBehaviour
{
    [SerializeField] protected GenericScriptableObject genericData;

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
        currentMovementSpeed = genericData.MovementSpeed;
        currentDamage = genericData.Damage;
        currentHealth = genericData.MaxHealth;
        invincibilityTimer = genericData.IFrameDuration;
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
        
        Damage(damage);

        if (currentHealth <= 0) 
        {
            Kill();
        }
        else
        {
            invincibilityTimer = genericData.IFrameDuration;
            isInvincible = true;
            immunityFlash.Flash(genericData.IFrameDuration);
        }
    }

    protected virtual void Damage(float damage)
    {
        currentHealth -= damage;
    }

    protected abstract void Kill();
}
