using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public RawImage CompassImage;
    public Transform Player;
    public Text CompassDirectionText; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //obtener el manejo de las imagenes uvRect
        CompassImage.uvRect = new Rect(Player.localEulerAngles.y / 360, 0, 1, 1);

        //oBTEMNER una copia del vector movimiento hacia adelante
        Vector3 forward = Player.transform.forward;

        //zero out el componente y del vector de movimienyo en el plano x,z
        forward.y = 0;

        //ajsuar los angulos para que estos se den en incrementos de 5 grados
        float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
        headingAngle = 1 * (Mathf.RoundToInt(headingAngle));

        //Convertir flotantes a entero
        int diaplayangle;
        diaplayangle = Mathf.RoundToInt(headingAngle);

        //ajustar el tecto de la brujula a la imagen de texto de la brujula
        switch (diaplayangle)
        {
            case 0:
                //haz lo sigueinte
                CompassDirectionText.text = "N";
                break;
            case 360:
                //haz lo sigueinte
                CompassDirectionText.text = "N";
                break;
            case 45:
                //haz lo sigueinte
                CompassDirectionText.text = "NE";
                break;
            case 90:
                //haz lo sigueinte
                CompassDirectionText.text = "E";
                break;
            case 130:
                //haz lo sigueinte
                CompassDirectionText.text = "SE";
                break;
            case 180:
                //haz lo sigueinte
                CompassDirectionText.text = "S";
                break;
            case 225:
                //haz lo sigueinte
                CompassDirectionText.text = "SW";
                break;
            case 270:
                //haz lo sigueinte
                CompassDirectionText.text = "W";
                break;
            default:
                CompassDirectionText.text = headingAngle.ToString();
                break;
                   
        }

         
         
    }
}