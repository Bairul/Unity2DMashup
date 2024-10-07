using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject baseData;

    [SerializeField]
    private ImmunityFlash immunityFlash;

    [SerializeField]
    private DamageIndicator damageIndicator;

    // Current Stats
    [HideInInspector]
    public float currentMovementSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    // iframe
    [HideInInspector]
    public float invincibilityTimer;
    public bool isInvincible;

    void Awake()
    {
        currentMovementSpeed = baseData.MovementSpeed;
        currentHealth = baseData.MaxHealth;
        currentDamage = baseData.Damage;
        invincibilityTimer = baseData.IFrameDuration;
        isInvincible = false;
    }

    public void CheckIFrame()
    {
        if (isInvincible)
        {
            if (invincibilityTimer > 0)
            {
                invincibilityTimer -= Time.deltaTime;
            }
            else
            {
                isInvincible = false;
            }
        }
    }

    public void TakeDamage(float damage) 
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            damageIndicator.ShowDamage(damage);

            if (currentHealth <= 0) 
            {
                Kill();
            }
            else
            {
                invincibilityTimer = baseData.IFrameDuration;
                isInvincible = true;
                immunityFlash.Flash(baseData.IFrameDuration);
            }
        }
    }

    public void Kill() 
    {
        Destroy(gameObject);
    }
}
