public class HpModifier : AttributeModifier
{
    protected override void ApplyModifier()
    {
        float healthPercentage = playerStats.currentHealth / playerStats.currentMaxHealth;

        playerStats.currentMaxHealth *= 1 + attributeData.Multiplier;
        playerStats.currentHealth = playerStats.currentMaxHealth * healthPercentage;
    }
}
