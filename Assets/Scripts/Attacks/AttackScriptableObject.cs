using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AttackScriptableObject", menuName ="ScriptableObjects/Attack")]
public class WeaponScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public float Speed { get => Speed; private set => Speed = value; }
}
