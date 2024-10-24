public class AtkModifier : AttributeModifier
{
    protected override void ApplyModifier()
    {
        playerStats.currentDamage *= 1 + attributeData.Multiplier;
    }
}
