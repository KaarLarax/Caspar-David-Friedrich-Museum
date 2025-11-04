using System.Collections;
using UnityEngine;

public class LightOnCollision : MonoBehaviour
{
    public Light collisionLight;  // Luz a controlar
    public float fadeDuration = 2f; // Tiempo en segundos para apagar la luz gradualmente

    private Coroutine fadeCoroutine;

    void Start()
    {
        if (collisionLight == null)
        {
            collisionLight = GetComponentInChildren<Light>(); // Busca una luz en los hijos
        }

        if (collisionLight != null)
        {
            collisionLight.intensity = 0f; // Inicia apagada
            collisionLight.enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionLight != null)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine); // Detiene el apagado si estaba ocurriendo
            collisionLight.enabled = true;
            collisionLight.intensity = 1f; // Enciende la luz completamente
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collisionLight != null)
        {
            fadeCoroutine = StartCoroutine(FadeOutLight());
        }
    }

    IEnumerator FadeOutLight()
    {
        float startIntensity = collisionLight.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            collisionLight.intensity = Mathf.Lerp(startIntensity, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        collisionLight.intensity = 0f;
        collisionLight.enabled = false;
    }
}
