using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject baseData;

    // Current Stats
    public float currentMovementSpeed;
    public float currentHealth;
    public float currentDamage;

    // Awake is called before start
    void Awake() 
    {
        currentMovementSpeed = baseData.MovementSpeed;
        currentHealth = baseData.MaxHealth;
        currentDamage = baseData.Damage;
    }
}
