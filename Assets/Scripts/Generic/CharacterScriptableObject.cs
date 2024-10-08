using UnityEngine;

[CreateAssetMenu(fileName ="CharacterScriptableObject", menuName ="ScriptableObjects/Character")]
public class CharacterScriptableObject : GenericScriptableObject
{
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
}
