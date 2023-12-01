using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuevaPrueba : MonoBehaviour
{
    public float velocidadInicial = 5f;
    public float impulsoAdicional = 10f;
    public float velocidadRotacion = 5f;
    

    private Rigidbody2D rb;
    private bool enSuelo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(velocidadInicial, 0f);
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) )
        {
            AplicarImpulsoProgresivo();
            rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        }

        float rotacion = -movimientoHorizontal * velocidadRotacion;
        rb.angularVelocity = rotacion;
    }

    void AplicarImpulsoProgresivo()
    {
        //dirreccion en la que mira el objeto
        Vector2 direccionMirada = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad), Mathf.Sin(rb.rotation * Mathf.Deg2Rad));

        // aplicamos impulso
        rb.velocity += direccionMirada * impulsoAdicional;
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
            {
            enSuelo = true;
            // Gravedad a su valor original cuando toca el suelo 
            rb.gravityScale = 1f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo") )
        {
            enSuelo = false;
            // Aumentamos gravedad con el suelo
            rb.gravityScale = 1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wave"))
        {
            enSuelo = false;
            // Aumentamos la gravedad cuando deja de tocar la wave
            rb.gravityScale = 3f;
        }
    }
}

