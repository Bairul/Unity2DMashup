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

    /// <summary>
    /// Character’s elemental affinity defines the skill pool
    /// <br></br>
    /// Ex. If a character has Fire and Water affinity, then their Skill pool will consist of Fire and Water type skills.
    /// <br></br>
    /// Character’s bloodline skills (basic, unique) are also added to the pool at the start.
    /// <br></br>
    /// Character’s passive can add or remove certain skills in the pool at the start.
    /// <br></br>
    /// Includes all attribute/supporting skills (%Atk, %Hp, %Mvt, etc) 
    /// </summary>
    /// <param name="elementalAffinities"></param>
    public void InitAvailableSkills(ElementalType[] elementalAffinities)
    {
        availableSkills = new List<WeightedObject>();

        // add elements
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

        // add attribues
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

    /// <summary>
    /// Player can choose from 3 or 4 skills after leveling
    /// <br></br>
    /// Each skill must be unique
    /// <br></br>
    /// Each skill is chosen based on weights
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Character’s weapon affects the weights of the skills in the pool
    /// <br></br>
    /// Ex. Increase/decrease the chance of getting a Fire type skill by 25%
    /// <br></br>
    /// Ex. Increase/decrease the chance of getting Fireball skill by 50%
    /// <br></br>
    /// Weights may be affected during runtime from artifacts or debuffsss 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="percentageChange"></param>
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
        // foreach (WeightedObject weightedObject in availableSkills)
        // {
        //     if (weightedObject.prefab.name.Equals(name))
        //     {
        //         // availableSkills.Remove(weightedObject);
        //         // TODO: Add next level prefab to available skills while keeping the same weight
        //         break;
        //     }
        // }
    }
}
