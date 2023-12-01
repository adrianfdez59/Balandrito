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
        // guardamos la rotación actual y el tiempo actual
        rotacionInicial = rb.rotation;
        tiempoInicioTransicion = Time.time;

        // Generar un valor aleatorio entre dos rangos
        float rotacionAleatoria = Random.Range(0, 2) == 0 ? rangoMinimo : rangoMaximo;

    }

    void ActualizarRotacion()
    {
        // Calcular el progreso de la transición (0 a 1)
        float progreso = (Time.time - tiempoInicioTransicion) / duracionTransicion;

        // Interpolar suavemente entre la rotación inicial y la nueva rotación
        float nuevaRotacion = Mathf.Lerp(rotacionInicial, rotacionAleatoria, progreso);

        // Aplicar la rotación al objeto
        rb.SetRotation(nuevaRotacion);

        // Si la transición ha terminado, cancelar la repetición del metodo
        if (progreso >= 1f)
        {
            CancelInvoke("ActualizarRotacion");
        }
    }
   
}

