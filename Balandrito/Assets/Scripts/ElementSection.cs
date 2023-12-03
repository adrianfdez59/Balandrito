using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tipo de elemento que es. Si es Roca o Vacio, se le asignará el currentLayer que corresponda
public enum ElementType { Rock, Wave, Empty }

public class ElementSection : MonoBehaviour
{
    // El funcionamiento de este script será similar al de SeaSection
    // Las secciones serán menores, y los elementos (rocas y olas) irán apareciendo con cierto margen
    // Estas variables de aparición se controlarán desde el ElementManager, y variarán por nivel
    // Desde el LevelManager se controla la velocidad por sección
    private float velocityX;

    public ElementType elementType;

    // Esta propiedad devuelve la anchura completa de la seccion
    // La anchura de la escena es 32, luego se decide que la sección para los elementos sea de 1/8 de 32: 4
    public float width;
    // Y su media anchura
    public float halfWidth { get { return width / 2; } }


    // Transform del objetivo a seguir. Es un valor por referencia, por lo que siempre dispondremos del valor actualizado
    // public Transform target;
    // Referencia a la cámara para calcular la distancia respecto a la sección 
    private Transform cameraTransform;

    // Controla si se ha solicitado la destrucción de la seccion
    private bool isDestroyed = false;


    // Start is called before the first frame update
    void Start()
    {
        // Recuperamos la referencia al transform de la cámara utilizando el main camera
        cameraTransform = Camera.main.transform;

        velocityX = LevelManager.instance.velocityXSections[getSectionNumber()];
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            MoveSection();
        }

        // Comprobamos si debemos borrar esta sección
        CheckDestroy();
    }

    private void OnDrawGizmos()
    {
        // El tamaño del objeto indicará qué objeto es
        switch (width)
        {
            // Ola
            case 8:
                Gizmos.color = Color.blue;
                break;
            // Roca 1 
            case 6:
                Gizmos.color = new Color(0.8f, 0.4f, 0f);
                break;
            // Roca 3
            case 4:
                Gizmos.color = new Color(0.6f, 0.3f, 0.3f);
                break;
            // Roca 2
            case 2:
                Gizmos.color = new Color(0.4f, 0.2f, 0.4f);
                break;
            default:
                Gizmos.color = Color.red;
                break;
        }

        // Dibujamos el gizmo. La altura será 2
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 2, 0f));
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Destroyer")
        {
            Debug.Log("Object has been anihilated");
            DestroySection();
        }
    }*/

    /// <summary>
    /// Evaluamos si la sección debe de ser destruida. Debe estar a nivel de Section, no de SectionManager
    /// </summary>
    private void CheckDestroy()
    {
        // Calculamos el lado izquierdo de la pantalla en el mundo:
        //      ortographicSize es la mitad del alto de la cámara
        //      screen.width es el ancho de la pantalla en píxeles
        //      screen.height es el alto de la pantalla en píxeles
        // Esta operación es una regla de 3
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
        // Utilizando el Singleton de SectionManager, llamamos al método de generación de una nueva sección
        ElementManager.instance.SpawnSection(getSectionNumber());

        // Indicamos a la sección que se autodestruya
        Destroy(gameObject, 2f);

        // Indicado que la sección ya ha sido destruida (esto lo llevará el recolector de basura)
        isDestroyed = true;
    }

    /// <summary>
    /// Movemos la sección hacia la derecha, con una velocidad dada
    /// </summary>
    private void MoveSection()
    {
        // Creo un vector de posición temporal
        Vector3 newPos = transform.position;

        newPos.x = transform.position.x - (velocityX * Time.deltaTime);

        transform.position = newPos;
    }

    /// <summary>
    /// Extraemos la sección (marea) en base al Layer sobre el que está el objeto: la más cercana es 0, y la más lejana 4
    /// </summary>
    private int getSectionNumber()
    {
        int sectionNumber;
        int layer = gameObject.layer;

        if (layer == LayerMask.NameToLayer("Section0"))
            sectionNumber = 0;
        else if (layer == LayerMask.NameToLayer("Section1"))
            sectionNumber = 1;
        else if (layer == LayerMask.NameToLayer("Section2"))
            sectionNumber = 2;
        else if (layer == LayerMask.NameToLayer("Section3"))
            sectionNumber = 3;
        else if (layer == LayerMask.NameToLayer("Section4"))
            sectionNumber = 4;
        else
            sectionNumber = -1;

        return sectionNumber;
    }

    /// <summary>
    /// Se le asigna el layer pasado como argumento entero y el sorting layer
    /// </summary>
    /// <param name="sectionNumber"></param>
    public void AssignLayer(int sectionNumber)
    {
        switch (sectionNumber)
        {
            case 0:
                gameObject.layer = LayerMask.NameToLayer("Section0");
                //gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section0");
                break;
            case 1:
                gameObject.layer = LayerMask.NameToLayer("Section1");
                //gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section1");
                break;
            case 2:
                gameObject.layer = LayerMask.NameToLayer("Section2");
                //gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section2");
                break;
            case 3:
                gameObject.layer = LayerMask.NameToLayer("Section3");
                //gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section3");
                break;
            case 4:
                gameObject.layer = LayerMask.NameToLayer("Section4");
                //gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section4");
                break;
            default:
                break;
        }
    }
}
