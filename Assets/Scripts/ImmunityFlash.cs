using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityFlash : MonoBehaviour
{
    [SerializeField]
    private Material flashMaterial;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    private IEnumerator FlashRoutine(float flashDuration)
    {
        spriteRenderer.material = flashMaterial;
        Debug.Log(flashDuration);

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

    public void Flash(float flashDuration)
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine(flashDuration));
    }
}
