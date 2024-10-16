using UnityEngine;

public enum EnemyType
{
    Flying,
    Armored,
    Flesh
}

[CreateAssetMenu(fileName ="EnemyScriptableObject", menuName ="ScriptableObjects/Enemy")]
public class EnemyScriptableObject : GenericScriptableObject
{
    [SerializeField]
    private EnemyType enemyType;
    public EnemyType EnemyType { get => enemyType; private set => enemyType = value; }

    [Header("Base Stats")]
    [SerializeField]
    private float attackCooldown;
    public float AttackCooldown { get => attackCooldown; private set => attackCooldown = value; }
}
