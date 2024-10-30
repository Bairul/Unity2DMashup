using UnityEngine;

[RequireComponent(typeof(SkillType))]
public abstract class AttributeModifier : MonoBehaviour
{
    [SerializeField]
    protected AttributeScriptableObject attributeData;

    protected PlayerStats playerStats;

    void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }

    void Start()
    {
        ApplyModifier();
    }

    protected abstract void ApplyModifier();
}
