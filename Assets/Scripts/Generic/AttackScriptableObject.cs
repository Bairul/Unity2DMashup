using UnityEngine;

[CreateAssetMenu(fileName ="AttackScriptableObject", menuName ="ScriptableObjects/Attack")]
public class AttackScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value;}

    [Header("Base Stats")]
    [SerializeField]
    private float speed;
    public float Speed { get => speed; private set => speed = value; }
    
    [SerializeField]
    private float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }
    
    [SerializeField]
    private float pierce;
    public float Pierce { get => pierce; private set => pierce = value; }
    
    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    private float lifespan;
    public float Lifespan { get => lifespan; private set => lifespan = value; }
}
