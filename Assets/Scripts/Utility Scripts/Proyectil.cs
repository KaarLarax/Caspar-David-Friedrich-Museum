using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    // Contador estático de objetos destruidos
    public static int objetosDestruidos = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ItemObstaculo"))
        {
            // Incrementar el contador de objetos destruidos
            objetosDestruidos++;

            // Si existe SC_FP_Shooter7, actualiza el HUD
            if (SC_FP_Shooter7.instancia != null)
            {
                SC_FP_Shooter7.instancia.IncrementarObjetosDestruidos();
            }

            // Destruir el objeto y el proyectil
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
