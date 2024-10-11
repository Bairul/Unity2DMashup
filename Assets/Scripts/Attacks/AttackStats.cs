using UnityEngine;

public class AttackStats : MonoBehaviour
{
    [SerializeField] private AttackScriptableObject baseData;
    public AttackScriptableObject BaseData {get => baseData;}

    // Current Stats
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public float currentPierce;
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
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
