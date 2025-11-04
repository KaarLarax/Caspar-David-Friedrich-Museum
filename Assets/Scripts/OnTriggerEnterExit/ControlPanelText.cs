using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelText : MonoBehaviour
{
    public GameObject panelTexto; // Asigna el panel en el Inspector

    private void Start()
    {
        // Verificar que panelTexto esté asignado
        if (panelTexto != null)
        {
            panelTexto.SetActive(false); // Asegura que el panel empiece oculto
        }
        else
        {
            Debug.LogError("Panel de texto no asignado en el inspector");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegura que el objeto tenga el tag
        {
            if (panelTexto != null)
            {
                panelTexto.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (panelTexto != null)
            {
                panelTexto.SetActive(false);
            }
        }
    }
}

/*
En este ejemplo:
Asegurate de que el objeto al que se adjunta este script tenga el panel de texto asignado al campo panelTexto en el inspector de UNITY.
Asegurate de que el objeto que activa y desactiva el panel de texto tenga este script adjunto,
Asegurate de que el objeto que entra y sale del trigger tenga el tag "Player" (o cualquier otro tag que desees) para activar o desactivar el panel de texto.
En el modo Start, se asegura de que el panel de texto este desactivado al inicio.
Con esto, el panel de texto se activara cuando el objeto con el tag "Player" entre en el trigger y se desactivara cuando el objeto salga del trigger.
Asegurate de que el panel de texto tenga un componente Text para mostrar el texto deseado.
*/

