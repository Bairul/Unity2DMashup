public class ExperienceOrb : Collectible
{
    public int experienceGranted;

    protected override void Collect(PlayerStats player)
    {
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }
}
