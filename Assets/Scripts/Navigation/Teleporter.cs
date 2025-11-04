using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class Teleporter : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string sceneName; // Nombre exacto de la escena a cargar

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Cambia a la escena indicada
            SceneManager.LoadScene(sceneName);
        }
    }
}