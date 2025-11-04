using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Teletransport : MonoBehaviour
{
    public Transform teleportTarget; //Destino de la teletransportacion
    public Text messageText; //Texto UI para el mensaje
    public float messageDuration = 4f; //Tiempo que el mensaje estará visible

    // Start is called before the first frame update
    void Start()
    {
        if (messageText != null)
            messageText.enabled = false; //ocultar mensaje
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            StartCoroutine(TeleportWithMessage(other));
    }

    private IEnumerator TeleportWithMessage(Collider player){
        if (messageText != null){
            messageText.text = "¡Teletransportando en 4 segundos!";
            messageText.enabled = true;
        }

        yield return new WaitForSeconds(messageDuration);

        if (messageText != null)
            messageText.enabled = false;

        player.transform.position = teleportTarget.position;
    }
}
