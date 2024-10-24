using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterScriptableObject", menuName ="ScriptableObjects/Character")]
public class CharacterScriptableObject : GenericScriptableObject
{
    [Range(0,1)]
    [SerializeField]
    private float critRate;
    public float CritRate { get => critRate; private set => critRate = value; }

    [SerializeField]
    private float critDamage;
    public float CritDamage { get => critDamage; private set => critDamage = value; }

    [SerializeField]
    private float magnetRange;
    public float MagnetRange { get => magnetRange; private set => magnetRange = value; }

    [Header("Experience")]
    // [SerializeField]
    // private ExperienceCapMode experienceCapMode;
    // public ExperienceCapMode ExperienceCapMode { get => experienceCapMode; }

    [SerializeField]
    private int lastLevel;
    public int LastLevel {get => lastLevel; }

    // [Header("Experience Range Cap Mode")]
    [SerializeField]
    private List<LevelRange> levelRanges;
    public List<LevelRange> LevelRanges { get => levelRanges; }

    // [Header("Experience Function Cap Mode")]
    // [SerializeField]
    // private float xpFunctionCoefficient;
    // public float XpFunctionCoefficient { get => xpFunctionCoefficient; }

    // [SerializeField]
    // private float xpFunctionExponent;
    // public float XpFunctionExponent { get => xpFunctionExponent; }

    // [SerializeField]
    // private float xpFunctionConstant;
    // public float XpFunctionConstant { get => xpFunctionConstant; }

    [Header("Skills")]
    [SerializeField]
    private ElementalType[] elementalAffinities;
    public ElementalType[] ElementalAffinities { get => elementalAffinities; }

    [SerializeField]
    private GameObject starterSkill;
    public GameObject StarterSkill { get => starterSkill; }

    [SerializeField]
    private GameObject dashSkill;
    public GameObject DashSkill { get => dashSkill; }
}
