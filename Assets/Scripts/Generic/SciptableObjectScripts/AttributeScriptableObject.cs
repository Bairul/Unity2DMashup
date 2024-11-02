using UnityEngine;

[CreateAssetMenu(fileName ="AttributeScriptableObject", menuName ="ScriptableObjects/Attribute")]
public class AttributeScriptableObject : ScriptableObject
{
    [Range(0,2)]
    [SerializeField]
    private float multiplier;
    public float Multiplier { get => multiplier; private set => multiplier = value; }
}
