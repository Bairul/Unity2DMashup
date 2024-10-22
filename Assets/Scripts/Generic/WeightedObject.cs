using UnityEngine;

[System.Serializable]
public class WeightedObject
{
    public GameObject prefab;
    public float weight;

    public WeightedObject(GameObject prefab, float weight)
    {
        this.prefab = prefab;
        this.weight = weight;
    }
}
