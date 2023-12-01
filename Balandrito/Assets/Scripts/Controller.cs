using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //fuerza torsion ajustabe en el editor
    public float fuerzaTorque = 3f;
    public Rigidbody2D rb;
    //lista de capas
    public float velocidadInicial = 5f;
    public float impulsoAdicional = 10f;
    public LayerMask[] capas;
    

    // registro de capas (nombradas de 0 a 4)
    private int capaActual = 0;

    // lista de posiciones correspondientes a cada capa
    public Vector3[] posiciones;
    

    void Start()
    {
        // Obtenemos el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(-6.834f, -2.04f, 0);
        rb.velocity = new Vector2(velocidadInicial, 0f);


        // Inicializamos la capa y posici�n inicial
        CambiarCapa(false);

    }

    void Update()
    {

        // Verificar si se presiona la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Aplicar impulso adicional hacia la derecha cuando se presiona la barra espaciadora
            rb.velocity += new Vector2(impulsoAdicional, 0f);
        }


        // Al pulsar la tecla W, cambiar a la siguiente capa
        if (Input.GetKeyDown(KeyCode.W))
        {
            CambiarCapa(true); // true indica cambio hacia adelante
        }

        // Al pulsar la tecla S, volver a la capa anterior
        if (Input.GetKeyDown(KeyCode.S))
        {
            CambiarCapa(false); // false indica cambio hacia atr�s
        }


        //transform.position = new Vector3(-6.834f, -2.04f, 0); (no hagas mucho caso a esto es para hacer pruebas
        // Obtener la entrada del teclado
        float rotacion = Input.GetAxis("Horizontal");

        // Calcular la fuerza de torque
        float torque = -rotacion * fuerzaTorque;

        // Limitar la rotaci�n a un m�ximo de 90 grados
        float nuevaRotacion = Mathf.Clamp(rb.rotation + torque * Time.deltaTime, -60f, 60f);

        // Aplicar la rotaci�n al barco
        rb.SetRotation(nuevaRotacion);
        rb.AddTorque(torque * Time.deltaTime);
    }
    void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto con el que colisiona tiene el tag "wave"
        if (other.CompareTag("wave"))
        {
            // Calcula el desplazamiento en la direcci�n derecha
            float desplazamiento = fuerzaTorque * Time.deltaTime;

            // Mueve este objeto a la derecha
            transform.Translate(new Vector3(desplazamiento, 0f, 0f));
        }
    }


    void CambiarCapa(bool haciaAdelante)
    {
        // Incrementar o decrementar la capa actual
        capaActual = haciaAdelante ? Mathf.Min(capaActual + 1, capas.Length - 1) : Mathf.Max(capaActual - 1, 0);

        // Aplicamos la capa actual al objeto
        gameObject.layer = (int)Mathf.Log(capas[capaActual].value, 2);

        // Cambiar la posici�n del objeto seg�n la nueva capa con la lista que tenemos
        if (capaActual < posiciones.Length)
        {
            transform.position = new Vector3(transform.position.x, posiciones[capaActual].y, posiciones[capaActual].z);
        }
    }
}
