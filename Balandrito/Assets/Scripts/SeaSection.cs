using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SeaSection : MonoBehaviour
{
    // Nombre de secci�n (marea): la m�s cercana es 0, y la m�s lejana 4
    public int sectionNumber;

    // Esta propiedad devuelve la anchura completa de la seccion
    public float width { get { return 32f; } }
    // Y su media anchura
    public float halfWidth { get { return width/2; } }

    // Velocidad de la secci�n 
    [Range(0f, 5f)]
    public float offsetX;
    // Transform del objetivo a seguir. Es un valor por referencia, por lo que siempre dispondremos del valor actualizado
    // public Transform target;

    // Referencia a la c�mara para calcular la distancia respecto a la secci�n 
    private Transform cameraTransform;


    // Controla si se ha solicitado la destrucci�n de la seccion
    private bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Recuperamos la referencia al transform de la c�mara utilizando el main camera
        cameraTransform = Camera.main.transform;

        //target = cameraTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null)
        {
            MoveSection();
        }

        // Comprobamos si debemos borrar esta secci�n
        CheckDestroy();
    }

    private void OnDrawGizmos()
    {
        // Si el n�mero de filas es par y el n�mero de columnas es par, mostraremos el gizmo en color verde. En caso contrario, en rojo
        if (width == 32)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        // Dibujamos el gizmo
        Gizmos.DrawWireCube(transform.position, new Vector3(32, 18, 0f));
    }

    /// <summary>
    /// Evaluamos si la secci�n debe de ser destruida. Debe estar a nivel de Section, no de SectionManager
    /// </summary>
    private void CheckDestroy()
    {
        // Calculamos el lado izquierdo de la pantalla en en mundo:
        //      ortographicSize es la mitad del alto de la c�mara
        //      screen.width es el ancho de la pantalla en p�xeles
        //      screen.height es el alto de la pantalla en p�xeles
        // Esta operaci�n es una regla de 3
        float leftSideOfScreen = cameraTransform.position.x - ((Camera.main.orthographicSize * Screen.width) / Screen.height);

        if (transform.position.x < (leftSideOfScreen - halfWidth) && !isDestroyed)
        {
            DestroySection();
        }
    }

    /// <summary>
    /// Destruye la seccion y se solicita generar una nueva
    /// </summary>
    private void DestroySection()
    {
        // Utilizando el Singleton de SectionManager, llamamos al m�todo de generaci�n de una nueva secci�n
        SeaSectionManager.instance.SpawnSection(sectionNumber);

        // Indicamos a la secci�n que se autodestruya
        Destroy(gameObject, 2f);

        // Indicado que la secci�n ya ha sido destruida (esto lo llevar� el recolector de basura)
        isDestroyed = true;
    }

    /// <summary>
    /// Movemos la secci�n hacia la derecha, con una velocidad dada
    /// </summary>
    private void MoveSection()
    {
        // Creo un vector de posici�n temporal
        Vector3 newPos = transform.position;

        newPos.x = transform.position.x - (offsetX * Time.deltaTime);

        transform.position = newPos;
    }
}
