using UnityEngine;

public interface ICollectible
{
    void Collect(PlayerStats player);
    void MoveTowards(Transform otherTransform);

    
}
