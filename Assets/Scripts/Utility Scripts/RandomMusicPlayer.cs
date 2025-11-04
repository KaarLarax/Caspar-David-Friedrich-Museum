using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    [Header("Lista de Canciones")]
    public AudioClip[] canciones;       // Aquí arrastras todas tus canciones
    private AudioSource audioSource;

    private int[] ordenAleatorio;
    private int indiceActual = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = false;

        // Genera un orden aleatorio al inicio
        GenerarOrdenAleatorio();

        // Empieza la primera canción
        ReproducirSiguiente();
    }

    void Update()
    {
        // Si ya terminó la canción, reproducir la siguiente
        if (!audioSource.isPlaying)
        {
            ReproducirSiguiente();
        }
    }

    void ReproducirSiguiente()
    {
        if (canciones.Length == 0) return;

        if (indiceActual >= canciones.Length)
        {
            // Si ya se tocaron todas, volvemos a generar un nuevo orden
            GenerarOrdenAleatorio();
            indiceActual = 0;
        }

        int indexCancion = ordenAleatorio[indiceActual];
        audioSource.clip = canciones[indexCancion];
        audioSource.Play();

        indiceActual++;
    }

    void GenerarOrdenAleatorio()
    {
        ordenAleatorio = new int[canciones.Length];
        for (int i = 0; i < canciones.Length; i++)
        {
            ordenAleatorio[i] = i;
        }

        // Mezcla estilo Fisher-Yates
        for (int i = 0; i < ordenAleatorio.Length; i++)
        {
            int randomIndex = Random.Range(i, ordenAleatorio.Length);
            int temp = ordenAleatorio[i];
            ordenAleatorio[i] = ordenAleatorio[randomIndex];
            ordenAleatorio[randomIndex] = temp;
        }
    }
}
