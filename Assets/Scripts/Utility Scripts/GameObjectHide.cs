using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameObjectHide : MonoBehaviour
{
 public GameObject objetoX;
    // Start is called before the first frame update
    void Start()
    {
        objetoX.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        HideGameObject();
    }

    void HideGameObject()
    {
       if (Input.GetKeyDown(KeyCode.G))
        {
            objetoX.gameObject.SetActive(true);
        }
      if (Input.GetKeyDown(KeyCode.H))
        {
          objetoX.gameObject.SetActive(false);
        } 
    }
}