using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public Vector2 offset;
    public GameObject prefab;
    public float duration;

    public void ShowDamage(float damage)
    {
        Vector2 pos = new(transform.position.x, transform.position.y);
        GameObject dmgTxt = Instantiate(prefab, pos + offset, Quaternion.identity);
        dmgTxt.GetComponentInChildren<TextMeshPro>().text = "" + (int) damage;
        Destroy(dmgTxt, duration);
    }
}
