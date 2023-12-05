using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntuacion : MonoBehaviour
{

    public Collider2D miCollider; // Asigna el collider en el Inspector de Unity
    private bool objetoEnCollider;
    private float tiempoTranscurrido;
    private int puntos = 0;
    public TextMeshProUGUI textMesh;

    void Start()
    {
        objetoEnCollider = false;
        tiempoTranscurrido = 0f;
    }

    void Update()
    {
        // Verifica si el objeto está dentro del collider
        if (objetoEnCollider)
        {
            // Incrementa el tiempo transcurrido
            tiempoTranscurrido += Time.deltaTime;

            // Incrementa los puntos cada segundo
            if (tiempoTranscurrido >= 0.2f)
            {
                puntos += 100;
                tiempoTranscurrido = 0f;
                Debug.Log("Puntos: " + puntos);
                textMesh.text = "Puntos:" + puntos.ToString("0");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entró en el collider es el que nos interesa
        if (other == miCollider)
        {
            objetoEnCollider = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verifica si el objeto que salió del collider es el que nos interesa
        if (other == miCollider)
        {
            objetoEnCollider = false;
            tiempoTranscurrido = 0f; // Reinicia el tiempo cuando el objeto sale del collider
        }
    }
}
