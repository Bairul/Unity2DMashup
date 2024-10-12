using UnityEngine;

public class GameWorld : MonoBehaviour
{
    // Singleton design
    public static GameWorld Instance { get; private set; }

    [SerializeField]
    private Transform playerTransform;
    public Transform PlayerTransform {get => playerTransform; }

    private GameObject nearestEnemy;
    private float nearestEnemyDistanceSq;

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
    }

    // updates before all Updates (specified in project settings)
    void Update()
    {
        nearestEnemyDistanceSq = float.MaxValue;
        nearestEnemy = null;
    }

    public void UpdateNearestEnemy(GameObject enemy)
    {
        Vector3 enemyPos = enemy.transform.position;
        Vector3 distance = playerTransform.position - enemyPos;
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
