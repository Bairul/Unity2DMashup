public class SpdModifier : AttributeModifier
{
    protected override void ApplyModifier()
    {
        playerStats.currentMovementSpeed *= 1 + attributeData.Multiplier;
    }
}
