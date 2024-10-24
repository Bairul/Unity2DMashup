using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillPool : MonoBehaviour
{
    [Header("Put every skill objects here")]
    [SerializeField]
    private GameObject[] allElementalSkills;

    [SerializeField]
    private GameObject[] allAttributeSkills;

    public List<WeightedObject> availableSkills;

    public void InitAvailableSkills(ElementalType[] elementalAffinities)
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

        WeightedObject.NormalizeWeights(availableSkills);
    }

    List<WeightedObject> CopyOfAvailableSkills()
    {
        List<WeightedObject> copy = new();
        foreach (WeightedObject weightedObject in availableSkills)
        {
            copy.Add(new WeightedObject(weightedObject.prefab, weightedObject.weight));
        }
        return copy;
    }

    public List<GameObject> GetSkills(int length)
    {
        List<GameObject> skills = new();
        List<WeightedObject> copy = CopyOfAvailableSkills();

        for (int i = 0; i < length; i++)
        {
            WeightedObject obj = WeightedObject.GetRandomWeightedObject(copy);

            if (obj != null && obj.prefab != null)
            {
                copy.Remove(obj);
                WeightedObject.NormalizeWeights(copy);
                Debug.Log(obj.prefab.name);

                skills.Add(obj.prefab);
            }
        }

        return skills;
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

        WeightedObject.NormalizeWeights(availableSkills);
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

        WeightedObject.NormalizeWeights(availableSkills);
    }

    public void LevelUpSkillInPool(string name)
    {
        foreach (WeightedObject weightedObject in availableSkills)
        {
            if (weightedObject.prefab.name.Equals(name))
            {
                availableSkills.Remove(weightedObject);
                // TODO: Add next level prefab to available skills while keeping the same weight
                break;
            }
        }
        // don't have to normalize bc weight is the same
    }
}
