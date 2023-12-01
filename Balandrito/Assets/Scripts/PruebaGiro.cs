using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaGiro : MonoBehaviour
{
    public Collider2D colliderOriginal;
    public Collider2D colliderWave;
    public Collider2D colliderSuelo;

    private void Start()
    {
        // Al inicio, guarda el collider original
        colliderOriginal = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wave"))
        {
            // Si toca un objeto con el tag "Wave", desactiva el collider original y activa el collider de "Wave"
            colliderOriginal.enabled = false;
            colliderWave.enabled = true;
        }
        else if (other.CompareTag("suelo"))
        {
            // Si toca un objeto con el tag "Suelo", activa el collider original y desactiva el collider de "Wave"
            colliderOriginal.enabled = true;
            colliderWave.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("wave"))
        {
            // Si deja de tocar un objeto con el tag "Wave", activa el collider original y desactiva el collider de "Wave"
            colliderOriginal.enabled = true;
            colliderWave.enabled = false;
        }
    }
}
