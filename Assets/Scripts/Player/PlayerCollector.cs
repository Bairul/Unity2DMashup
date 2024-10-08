using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private Transform player;
    private CircleCollider2D range;
    public CircleCollider2D Range {get => range; set => range = value; }

    void Start()
    {
        range = GetComponent<CircleCollider2D>();
        player = GetComponentInParent<Transform>();
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out Collectible collectible))
        {
            collectible.MoveTowards(player);
        }
    }
}
