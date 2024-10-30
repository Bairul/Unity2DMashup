using UnityEngine;

public class SkillType : MonoBehaviour
{
    [SerializeField]
    private SkillOfType type;

    [SerializeField]
    private string skillName;
    public string SkillName { get => skillName; private set => skillName = value; }

    public bool IsTypeEquals(SkillOfType other)
    {
        return type == other;
    }

    public bool IsSameName(SkillType other)
    {
        return skillName.Equals(other.skillName);
    }
}
