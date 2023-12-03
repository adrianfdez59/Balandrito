using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SeaSection : MonoBehaviour
{ 
    // Esta propiedad devuelve la anchura completa de la seccion
    public float width { get { return 32f; } }
    // Y su media anchura
    public float halfWidth { get { return width/2; } }

    // Velocidad y dirección a la que la marea se desplaza en X
    private float velocityX;
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
        if(gameObject != null)
        {
            MoveSection();
        }

        // Comprobamos si debemos borrar esta sección
        CheckDestroy();
    }

    private void OnDrawGizmos()
    {
        // Si el tamáño es 32, se muestra verde. En caso contrario, en rojo
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
    /// Evaluamos si la sección debe de ser destruida. Debe estar a nivel de Section, no de SectionManager
    /// </summary>
    private void CheckDestroy()
    {
        // Calculamos el lado izquierdo de la pantalla en en mundo:
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
        SeaSectionManager.instance.SpawnSection(getSectionNumber());

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
}
