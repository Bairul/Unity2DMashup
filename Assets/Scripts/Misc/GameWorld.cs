using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    // Singleton design
    public static GameWorld Instance { get; private set; }

    [SerializeField]
    private PlayerController playerController;

    [Range(0,1)]
    [SerializeField]
    private float elementalDamageBonus;

    [Range(0,1)]
    [SerializeField]
    private float elementalDamageResist;

    private GameObject nearestEnemy;
    private float nearestEnemyDistanceSq;

    private Dictionary<string, float> elementalDamageTable;

    // Getters
    public Transform GetPlayerTransform {get => playerController.gameObject.transform; }
    public PlayerController GetPlayerController {get => playerController; }

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        elementalDamageTable = new Dictionary<string, float>
        {
            { ElementalType.Fire.ToString() + EnemyType.Armored.ToString(), 1f - elementalDamageResist },
            { ElementalType.Fire.ToString() + EnemyType.Flesh.ToString(), 1f + elementalDamageBonus },
            { ElementalType.Fire.ToString() + EnemyType.Flying.ToString(), 1f },

            { ElementalType.Water.ToString() + EnemyType.Armored.ToString(), 1f + elementalDamageBonus / 2f },
            { ElementalType.Water.ToString() + EnemyType.Flesh.ToString(), 1f + elementalDamageBonus / 2f },
            { ElementalType.Water.ToString() + EnemyType.Flying.ToString(), 1f + elementalDamageBonus / 2f },

            { ElementalType.Earth.ToString() + EnemyType.Armored.ToString(), 1f + elementalDamageBonus },
            { ElementalType.Earth.ToString() + EnemyType.Flesh.ToString(), 1f },
            { ElementalType.Earth.ToString() + EnemyType.Flying.ToString(), 1f - elementalDamageResist },

            { ElementalType.Wind.ToString() + EnemyType.Armored.ToString(), 1f },
            { ElementalType.Wind.ToString() + EnemyType.Flesh.ToString(), 1f - elementalDamageResist },
            { ElementalType.Wind.ToString() + EnemyType.Flying.ToString(), 1f + elementalDamageBonus }
        };
    }

    // updates before all Updates (specified in project settings)
    void Update()
    {
        nearestEnemyDistanceSq = float.MaxValue;
        nearestEnemy = null;
    }

    public float GetElementalDamageModifier(ElementalType elementalType, EnemyType enemyType)
    {
        if (elementalDamageTable.TryGetValue(elementalType.ToString() + enemyType.ToString(), out float damageModifier))
        {
            return damageModifier;
        }
        return 1.0f;
    }

    public void UpdateNearestEnemy(GameObject enemy)
    {
        Vector3 enemyPos = enemy.transform.position;
        Vector3 distance = GetPlayerTransform.position - enemyPos;
        float distanceSq = distance.sqrMagnitude;

        if (distanceSq < nearestEnemyDistanceSq)
        {
            nearestEnemyDistanceSq = distanceSq;
            nearestEnemy = enemy;
        }
    }

    public GameObject GetNearestEnemyWithinRange(float range)
    {
        float rangeSq = range * range;

        if (nearestEnemyDistanceSq < rangeSq)
        {
            return nearestEnemy;
        }
        
        return null;
    }
}
