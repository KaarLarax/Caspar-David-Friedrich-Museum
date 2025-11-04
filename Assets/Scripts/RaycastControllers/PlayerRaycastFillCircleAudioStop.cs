using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycastFillCircleAudioStop : MonoBehaviour
{
    public float raycastDistance = 10f;
    public LayerMask selectableLayer;
    public Image fillCircle;
    public float fillSpeed = 1f;
    public float fillThreshold = 1f;

    private GameObject lastHitObject;
    private float fillAmount = 0f;
    private bool isFilling = false;
    private bool audioPlayed = false; // Controla si el audio ya se reprodujo
    private AudioSource currentAudioSource; // Referencia al AudioSource actual

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, selectableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != lastHitObject)
            {
                fillAmount = 0f;
                fillCircle.fillAmount = fillAmount;
                isFilling = true;
                fillCircle.gameObject.SetActive(true);

                lastHitObject = hitObject;
                audioPlayed = false; // Reinicia el control de reproducción del audio
            }

            if (isFilling)
            {
                FillCircle();

                if (fillAmount >= fillThreshold && !audioPlayed)
                {
                    PlayAudio(hitObject);
                    audioPlayed = true; // Establece que el audio se ha reproducido
                }
            }
        }
        else
        {
            ResetCircle();
        }

        // Detener la reproducción del audio cuando el raycast deja de tocar el objeto
        if (lastHitObject != null && lastHitObject != hit.collider.gameObject)
        {
            StopAudio();
        }
    }

    void FillCircle()
    {
        fillAmount += fillSpeed * Time.deltaTime;
        fillAmount = Mathf.Clamp01(fillAmount);
        fillCircle.fillAmount = fillAmount;
    }

    void ResetCircle()
    {
        fillAmount = 0f;
        fillCircle.fillAmount = fillAmount;
        lastHitObject = null;
        isFilling = false;
        fillCircle.gameObject.SetActive(false);

        StopAudio();
    }

    void PlayAudio(GameObject obj)
    {
        AudioSource objAudioSource = obj.GetComponent<AudioSource>();
        if (objAudioSource != null)
        {
            currentAudioSource = objAudioSource;
            currentAudioSource.Play();
        }
    }

    void StopAudio()
    {
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
        }
    }
}