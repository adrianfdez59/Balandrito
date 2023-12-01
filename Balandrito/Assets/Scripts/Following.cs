using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{

    public Transform objetoAseguir;

    void Update()
    {
        
        if (objetoAseguir != null)
        {
            
            transform.position = objetoAseguir.position;
        }
    }
}
