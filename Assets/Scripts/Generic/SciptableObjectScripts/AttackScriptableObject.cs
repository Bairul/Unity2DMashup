using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public enum ElementalType
{
    Arcane,
    Fire,
    Water,
    Earth,
    Wind
}

public enum SkillType{
    Auto,
    Active
}

[CreateAssetMenu(fileName ="AttackScriptableObject", menuName ="ScriptableObjects/Attack")]
public class AttackScriptableObject : ScriptableObject
{
    [SerializeField]
    private ElementalType element;
    public ElementalType Element { get => element; private set => element = value; }

    [SerializeField]
    private SkillType skillType;
    public SkillType SkillType { get => skillType; }

    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    [Header("Base Stats")]
    [SerializeField]
    private float speed;
    public float Speed { get => speed; private set => speed = value; }
    
    [SerializeField]
    private float pierce;
    public float Pierce { get => pierce; private set => pierce = value; }
    
    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    private float lifespan;
    public float Lifespan { get => lifespan; private set => lifespan = value; }

    [SerializeField]
    private float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    private float range;
    public float Range { get => range; private set => range = value; }

    [SerializeField]
    private float count;
    public float Count { get => count; private set => count = value; }

    [Header("Crowd Control")] // do this later
    [SerializeField]
    private float knockBack;
    public float KnockBack { get => knockBack; private set => knockBack = value; }

    [SerializeField]
    private float knockUp;
    public float KnockUp { get => knockUp; private set => knockUp = value; }

    [SerializeField]
    private float stunDuration;
    public float StunDuration { get => stunDuration; private set => stunDuration = value; }
}
