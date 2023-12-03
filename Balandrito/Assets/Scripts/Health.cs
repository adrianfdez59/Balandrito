using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject personaje;
    public float alturaMaxima = 10f;
    public Collider2D izquierda;
    public Collider2D derecha;
    public bool objetoEnCollider;
    private float tiempoTranscurrido;
    public int vida = 30;
    public Transform objetoAseguir;

    void Start()
    {
        objetoEnCollider = false;
        tiempoTranscurrido = 0f;
    }

    void Update()
    {
        if (vida == 0)
        {
            Destroy(personaje);
            Debug.Log("Has perdido");
        }
        if (objetoAseguir != null)
        {
            // Obtener la posición actual del objeto a seguir
            Vector3 posicionObjetoAseguir = objetoAseguir.position;

            // Verificar si el objeto a seguir está por debajo de la altura máxima
            if (posicionObjetoAseguir.y < alturaMaxima)
            {
                // Mantener la misma posición en el eje Y - 2 de altura 
                Vector3 nuevaPosicion = transform.position;
                nuevaPosicion.y = posicionObjetoAseguir.y - 1f;

                // Asignar la nueva posición al objeto que tiene este script
                transform.position = nuevaPosicion;
            }
            else
            {
                // Si el objeto a seguir está por encima de la altura máxima, dejar de seguirlo
                Debug.DrawLine(transform.position, objetoAseguir.position, Color.red);
            }
        }
        // Verifica si el objeto está dentro del collider
        if (objetoEnCollider)
        {
            // Incrementa el tiempo transcurrido
            tiempoTranscurrido += Time.deltaTime;

            // Incrementa los puntos cada medio segundo
            if (tiempoTranscurrido >= 0.6f)
            {
                vida -= 2;
                tiempoTranscurrido = 0f;
                Debug.Log("Vida: " + vida);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entró en el collider es el que nos interesa
        if (other == izquierda || derecha)
        {
            objetoEnCollider = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verifica si el objeto que salió del collider es el que nos interesa
        if (other == izquierda || derecha)
        {
            objetoEnCollider = false;
            tiempoTranscurrido = 0f; // Reinicia el tiempo cuando el objeto sale del collider
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Rock"))
        {
            vida -= 20;
        }
        if (collision.gameObject.CompareTag("Bottle"))
        {
            vida += 15;
        }
    }
}
