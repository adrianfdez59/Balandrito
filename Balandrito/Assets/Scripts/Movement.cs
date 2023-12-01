using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocidad = 5f; // Ajusta la velocidad seg�n tus necesidades

    void Update()
    {
        // Calcula el desplazamiento en la direcci�n izquierda
        float desplazamiento = -1f * velocidad * Time.deltaTime;

        // Mueve el objeto en la direcci�n izquierda y fija la posici�n en Y
        transform.Translate(new Vector3(desplazamiento, 0f, 0f));

        // Fija la posici�n en Y para que no se mueva verticalmente
        transform.position = new Vector3(transform.position.x, -3.31f, transform.position.z);
    }
}