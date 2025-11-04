using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    public Transform target;          // jugador
    public float height = 50f;        // Y de la cámara
    public bool rotateWithTarget = true;

    void LateUpdate()
    {
        if (target == null) return;

        // mantener posición X,Z del target y altura fija
        Vector3 pos = target.position;
        transform.position = new Vector3(pos.x, height, pos.z);

        // rotar para que el minimapa gire con el jugador
        if (rotateWithTarget)
        {
            transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
