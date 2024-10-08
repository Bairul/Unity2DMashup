using UnityEngine;

public class PlayerStats : GenericStats
{
    public CharacterScriptableObject BaseData {get => (CharacterScriptableObject) genericData; }
    
    protected override void Awake() 
    {
        base.Awake();
    }

    protected override void Kill() 
    {
        Debug.Log("You Died");
    }
}
