using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float velocidad = 3f; // Ajusta la velocidad según tus necesidades

    void Update()
    {
        
        float desplazamiento = -1f * velocidad * Time.deltaTime;

       
        transform.Translate(new Vector3(desplazamiento, 0f, 0f));
    }
    }
