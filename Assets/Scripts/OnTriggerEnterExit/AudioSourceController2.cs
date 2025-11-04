using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController2 : MonoBehaviour
{
    public GameObject planoAudio;
    public AudioSource audioSource; // Asegúrate de asignar el AudioSource en el inspector

    private void Start()
    {
        // Verificar que audioSource esté asignado
        if (audioSource == null)
        {
            Debug.LogError("AudioSource no asignado en el inspector");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !audioSource.isPlaying)
        {
            audioSource.Play(); // Reproducir solo si no está ya sonando
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop();
        }
    }
}
