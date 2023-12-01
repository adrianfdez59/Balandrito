using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private Rigidbody2D rb;
    public float rangoMinimo = -700f;
    public float rangoMaximo = 700f;
    public float duracionTransicion = 0.5f;
    
    private float tiempoInicioTransicion;
    private float rotacionInicial;
    private float rotacionAleatoria;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        //  metodo cada 7 segundos
        InvokeRepeating("IniciarTransicion", 0f, 7f);

    }

    void IniciarTransicion()
    {
        // guardamos la rotaci�n actual y el tiempo actual
        rotacionInicial = rb.rotation;
        tiempoInicioTransicion = Time.time;

        // Generar un valor aleatorio entre dos rangos
        float rotacionAleatoria = Random.Range(0, 2) == 0 ? rangoMinimo : rangoMaximo;

    }

    void ActualizarRotacion()
    {
        // Calcular el progreso de la transici�n (0 a 1)
        float progreso = (Time.time - tiempoInicioTransicion) / duracionTransicion;

        // Interpolar suavemente entre la rotaci�n inicial y la nueva rotaci�n
        float nuevaRotacion = Mathf.Lerp(rotacionInicial, rotacionAleatoria, progreso);

        // Aplicar la rotaci�n al objeto
        rb.SetRotation(nuevaRotacion);

        // Si la transici�n ha terminado, cancelar la repetici�n del metodo
        if (progreso >= 1f)
        {
            CancelInvoke("ActualizarRotacion");
        }
    }
   
}

