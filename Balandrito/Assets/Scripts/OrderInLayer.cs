using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayer : MonoBehaviour
{

    public KeyCode teclaSubir = KeyCode.W;
    public KeyCode teclaBajar = KeyCode.S;
    public int[] capas;
    private int indiceCapaActual = 5;

    void Update()
    {
        if (Input.GetKeyDown(teclaSubir))
        {
            CambiarCapa(true);
        }
        else if (Input.GetKeyDown(teclaBajar))
        {
            CambiarCapa(false);
        }
    }

    void CambiarCapa(bool haciaArriba)
    {
        int nuevoIndice;

        if (haciaArriba)
        {
            nuevoIndice = (indiceCapaActual + 1) % capas.Length;
        }
        else
        {
            nuevoIndice = (indiceCapaActual - 1 + capas.Length) % capas.Length;
        }

        if (nuevoIndice != capas.Length - 1 && nuevoIndice != 0)
        {
            // Evitar cambiar directamente del primer al último layer
            indiceCapaActual = nuevoIndice;
            gameObject.layer =  capas[indiceCapaActual];
        }
    }
}
