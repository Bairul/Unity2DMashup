using UnityEngine;

public class HealthOrb : Collectible
{
    public float healingAmount;

    protected override void Collect(PlayerStats player)
    {
        player.currentHealth = Mathf.Min(player.currentHealth + healingAmount, player.currentMaxHealth);
        Destroy(gameObject);
    }
}
