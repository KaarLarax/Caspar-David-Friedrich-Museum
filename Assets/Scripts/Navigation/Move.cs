using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float deltaMovement = 10f;
    public float deltaRotation = 70f;
    public float X = 1;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        rota();
    }

    void movement()
    {
        //transform.Translate(new Vector3(X,1,1) * Time.deltaTime); // Transformacion geometrica relativa 
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * deltaMovement * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * deltaMovement * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * deltaMovement * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * deltaMovement * Time.deltaTime);    
    }

    void rota()
    {
      if (Input.GetKey(KeyCode.Q))
        transform.Rotate(new Vector3(0f, -deltaRotation * Time.deltaTime, 0f));
      if (Input.GetKey(KeyCode.E))
        transform.Rotate(new Vector3(0f, deltaRotation * Time.deltaTime, 0f));
    }
}
