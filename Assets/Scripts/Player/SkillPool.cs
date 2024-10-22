using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillPool : MonoBehaviour
{
    [Header("Put every skill objects here")]
    [SerializeField]
    private List<GameObject> allElementalSkills;

    [SerializeField]
    private List<GameObject> allAttributeSkills;

    public List<WeightedObject> availableSkills;

    public void InitAvailableSkills(List<ElementalType> elementalAffinities)
    {
        availableSkills = new List<WeightedObject>();

        foreach (GameObject skillObject in allElementalSkills)
        {
            AttackController skill = skillObject.GetComponent<AttackController>();

            foreach (ElementalType type in elementalAffinities)
            {
                if (skill.PreGetElementalType() == type)
                {
                    availableSkills.Add(new WeightedObject(skillObject, 1));
                }
            }
        }

        foreach (GameObject skillObject in allAttributeSkills)
        {
            availableSkills.Add(new WeightedObject(skillObject, 1));
        }

        // add bloodline here

        NormalizeWeights(availableSkills);
    }

    void NormalizeWeights(List<WeightedObject> list)
    {
        if (list == null) return;

        float totalWeight = 0f;
        foreach (WeightedObject weightedObject in list)
        {
            totalWeight += weightedObject.weight;
        }

        foreach (WeightedObject weightedObject in list)
        {
            weightedObject.weight /= totalWeight;
        }
    }

    GameObject GetRandomSkillBasedOnWeight(List<WeightedObject> list)
    {
        // Calculate the total weight
        float totalWeight = 0;
        foreach (WeightedObject obj in list)
        {
            totalWeight += obj.weight;
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0;

        foreach (WeightedObject obj in list)
        {
            cumulativeWeight += obj.weight;
            if (randomValue < cumulativeWeight)
            {
                list.Remove(obj);
                NormalizeWeights(list);
                return obj.prefab;
            }
        }

        Debug.LogError("Bad weight");
        return null; // Fallback, this line should never be reached
    }

    public void AdjustWeightOfType(ElementalType type, float percentageChange)
    {
        foreach (WeightedObject weightedObject in availableSkills)
        {
            AttackController skill = weightedObject.prefab.GetComponent<AttackController>();

            if (skill.PreGetElementalType() == type)
            {
                weightedObject.weight *= percentageChange;
            }
        }

        NormalizeWeights(availableSkills);
    }

    public void AdjustWeightOfSkill(string name, float percentageChange)
    {
        foreach (WeightedObject weightedObject in availableSkills)
        {
            if (weightedObject.prefab.name.Equals(name))
            {
                weightedObject.weight *= percentageChange;
            }
        }

        NormalizeWeights(availableSkills);
    }

    public List<GameObject> GetSkills(int length)
    {
        List<GameObject> skills = new();
        List<WeightedObject> copy = availableSkills.ToList();

        for (int i = 0; i < length; i++)
        {
            skills.Add(GetRandomSkillBasedOnWeight(copy));
        }

        return skills;
    }

    public void RemoveSkillFromPool(string name)
    {
        foreach (WeightedObject weightedObject in availableSkills)
        {
            if (weightedObject.prefab.name.Equals(name))
            {
                availableSkills.Remove(weightedObject);
                break;
            }
        }

        NormalizeWeights(availableSkills);
    }
}
