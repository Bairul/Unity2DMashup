using UnityEngine;

[CreateAssetMenu(fileName ="CharacterScriptableObject", menuName ="ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameObject startingSkill;
    public GameObject StartingSkill { get => startingSkill; private set => startingSkill = value;}

    [Header("Base Stats")]
    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed { get => movementSpeed; private set => movementSpeed = value; }
    
    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    private float iFrameDuration;
    public float IFrameDuration { get => iFrameDuration; private set => iFrameDuration = value; }
    
    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    private float dashCooldown;
    public float DashCooldown { get => dashCooldown; private set => dashCooldown = value; }

    [SerializeField]
    private float dashDuration;
    public float DashDuration { get => dashDuration; private set => dashDuration = value; }

    [SerializeField]
    private float dashSpeed;
    public float DashSpeed { get => dashSpeed; private set => dashSpeed = value; }
}
