using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject baseData;

    [SerializeField]
    private ImmunityFlash immunityFlash;

    // Current Stats
    [HideInInspector]
    public float currentMovementSpeed;
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public float invincibilityTimer;
    public bool isInvincible;

    // Awake is called before start
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
            invincibilityTimer = baseData.IFrameDuration;
            isInvincible = true;
            immunityFlash.Flash(baseData.IFrameDuration);

            if (currentHealth <= 0) 
            {
                GameOver();
            }
        }
    }

    public void GameOver() 
    {
        Debug.Log("You Died");
    }
}
