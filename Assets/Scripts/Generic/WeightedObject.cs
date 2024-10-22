using UnityEngine;

[System.Serializable]
public class WeightedObject
{
    public GameObject prefab;
    public float weight;

    public WeightedObject(GameObject prefab, int weight)
    {
        this.prefab = prefab;
        this.weight = weight;
    }
}
