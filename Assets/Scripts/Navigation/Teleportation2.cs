using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class Teleportation2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform teleportTarget; // Destino de teleportación
    public Text messageText; // Texto del mensaje
    public float messageDuration = 4f; // Duración del mensaje en segundos
    void Start()
    {
        if (messageText != null)
        {
            messageText.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && teleportTarget != null)
        {
            StartCoroutine(TeleportWithCountdown(other));
        }
        else if (teleportTarget == null)
        {
            Debug.LogWarning("El destino de teleportación no está asignado.");
        }
    }

    private IEnumerator TeleportWithCountdown(Collider player)
    {
        // Obtener componentes
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        SC_FP_Shooter7 movementScript = player.GetComponent<SC_FP_Shooter7>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.isKinematic = true; // Congela al jugador
        }

        if (movementScript != null)
        {
            movementScript.enabled = false; // Desactiva el script de movimiento
        }

        if (messageText != null)
        {
            messageText.enabled = true;
            for (int i = (int)messageDuration; i > 0; i--)
            {
                messageText.text = "Teleportandose en " + i + " segundos...";
                yield return new WaitForSecondsRealtime(1f);
            }
        }

        // Teleportar al jugador
        if (playerRb != null)
        {
            playerRb.position = teleportTarget.position;
        }
        else
        {
            player.transform.position = teleportTarget.position;
        }

        if (playerRb != null)
        {
            playerRb.isKinematic = false; // Descongela al jugador
        }

        if (movementScript != null)
        {
            movementScript.enabled = true; // Reactiva el script de movimiento
        }
        if (messageText != null)
        {
            messageText.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
