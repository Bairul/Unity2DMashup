using System.Collections.Generic;
using UnityEngine;

public enum ExperienceCapMode
{
    FixedByLevelRange,
    FunctionScaling
}

[CreateAssetMenu(fileName ="CharacterScriptableObject", menuName ="ScriptableObjects/Character")]
public class CharacterScriptableObject : GenericScriptableObject
{
    [Range(0,1)]
    [SerializeField]
    private float critRate;
    public float CritRate { get => critRate; private set => critRate = value; }

    [Range(1,15)]
    [SerializeField]
    private float critDamage;
    public float CritDamage { get => critDamage; private set => critDamage = value; }

    [SerializeField]
    private float magnetRange;
    public float MagnetRange { get => magnetRange; private set => magnetRange = value; }

    [SerializeField]
    private GameObject startingSkill;
    public GameObject StartingSkill { get => startingSkill; private set => startingSkill = value;}

    [Header("Dash Stats")]
    [SerializeField]
    private float dashCooldown;
    public float DashCooldown { get => dashCooldown; private set => dashCooldown = value; }

    [SerializeField]
    private float dashDuration;
    public float DashDuration { get => dashDuration; private set => dashDuration = value; }

    [SerializeField]
    private float dashSpeedMultiplier;
    public float DashSpeedMultiplier { get => dashSpeedMultiplier; private set => dashSpeedMultiplier = value; }

    [Header("Experience")]
    [SerializeField]
    private ExperienceCapMode experienceCapMode;
    public ExperienceCapMode ExperienceCapMode { get => experienceCapMode; }

    [SerializeField]
    private int lastLevel;
    public int LastLevel {get => lastLevel; }

    [Header("Experience Range Cap Mode")]
    [SerializeField]
    private List<LevelRange> levelRanges;
    public List<LevelRange> LevelRanges { get => levelRanges; }

    [Header("Experience Function Cap Mode")]
    [SerializeField]
    private float xpFunctionCoefficient;
    public float XpFunctionCoefficient { get => xpFunctionCoefficient; }

    [SerializeField]
    private float xpFunctionExponent;
    public float XpFunctionExponent { get => xpFunctionExponent; }
    [SerializeField]
    private float xpFunctionConstant;
    public float XpFunctionConstant { get => xpFunctionConstant; }
}
