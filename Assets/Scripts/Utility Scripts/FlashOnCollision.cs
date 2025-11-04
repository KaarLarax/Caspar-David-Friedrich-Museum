using System.Collections;
using UnityEngine;

public class FlashOnCollision : MonoBehaviour
{
    public Color flashColor = Color.red; // Color del destello
    public float flashDuration = 0.1f;   // Duración del destello
    private Renderer objRenderer;
    private Color originalColor;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
        {
            originalColor = objRenderer.material.color;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (objRenderer != null)
        {
            StartCoroutine(FlashEffect());
        }
    }

    IEnumerator FlashEffect()
    {
        objRenderer.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        objRenderer.material.color = originalColor;
    }
}
