using UnityEngine;

public class GenericScriptableObject : ScriptableObject
{
    [Header("Base Stats")]
    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed { get => movementSpeed; private set => movementSpeed = value; }
    
    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    private float iFrameDuration;
    public float IFrameDuration { get => iFrameDuration; private set => iFrameDuration = value; }
    
    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }
}
