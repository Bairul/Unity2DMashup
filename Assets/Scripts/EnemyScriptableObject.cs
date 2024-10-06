using UnityEngine;

[CreateAssetMenu(fileName ="EnemyScriptableObject", menuName ="ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value;}

    [Header("Base Stats")]
    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed { get => movementSpeed; private set => movementSpeed = value; }
    
    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    
    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    private float attackCooldown;
    public float AttackCooldown { get => attackCooldown; private set => attackCooldown = value; }
}
