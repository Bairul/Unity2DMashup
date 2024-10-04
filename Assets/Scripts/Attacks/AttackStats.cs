using UnityEngine;

public class AttackStats : MonoBehaviour
{
    public AttackScriptableObject baseData;

    // Current Stats
    public float currentSpeed;
    public float currentPierce;
    public float currentDamage;
    public float currentCooldown;

    void Awake()
    {
        currentSpeed = baseData.Speed;
        currentPierce = baseData.Pierce;
        currentDamage = baseData.Damage;
        currentCooldown = baseData.CooldownDuration;
    }

    public bool CanAttack() 
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            ResetCooldown();
            return true;
        }
        return false;
    }

    public void ResetCooldown() 
    {
        currentCooldown = baseData.CooldownDuration;
    }

    public void ReducePierce() 
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
