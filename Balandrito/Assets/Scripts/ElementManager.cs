using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class ElementManager : MonoBehaviour
{
    // Array de secciones disponibles
    // El nombre del array hace referencia a la secci�n, y dentro se almacena cada tipo de ola
    [Header("Prefabs: Waves")]
    public ElementSection[] wave0Elements;
    public ElementSection[] wave1Elements;
    public ElementSection[] wave2Elements;
    public ElementSection[] wave3Elements;
    public ElementSection[] wave4Elements;
    [Header("Prefabs: Rocks")]
    public ElementSection[] rockElements;
    [Header("Prefabs: Empty Section")]
    public ElementSection emptyElement;

    [Header("Sections")]
    // Transform que contendr� las secciones generadas
    public Transform sectionContainer;
    // �ltima secci�n generada
    // public SeaSection currentSection;
    //N�mero de plataformas generadas inicialmente
    public int initialPrewarm = 20;
    // Array con las secciones actuales
    public ElementSection[] currentSections;
    // Trasladamos las posiciones iniciales de cada secci�n
    public float[] initialPositionY;

    // Probabilidades de aparici�n de cada elemento
    [Header("Probability: the three of them must sum 1.0")]
    public float emptyProbability;
    public float waveProbability;
    public float rockProbability;

    // PATR�N SINGLETON
    // Creamos una variable p�blica y est�tica
    public static ElementManager instance;

    private void Awake()
    {
        // Verificamos is la instancia es nula. De ser as� significa que no se ha creado antes
        // y que esta es la primera
        if (instance == null)
        {
            // Hacemos que esta instancia quede referida como est�tica, pudiendo acceder a ella sin referencia alguna
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //// CHAPUZA: si no suman 1, se aplica una probabilidad por defecto
        //if (emptyProbability + waveProbability + rockProbability != 1f)
        //{
        //    emptyProbability = 1f;
        //    waveProbability = 0f;
        //    rockProbability = 0f;
        //}

        // Si no tenemos un container, lo asignamos
        if (!sectionContainer) 
            sectionContainer = transform;

        // Crearemos 5 secciones por cada initialPrewarm marcado
        //for (int i = 0; i < 5; i++)
        //{
        //    // Para el elemento de la secci�n marcamos la posici�n de inicio recogiendo de initialPositionY
        //    SpawnEmptySection(i, true);

        //    for (int j = 0; j < initialPrewarm; j++)
        //    {
        //        // Debug.Log("Seccion: " + i.ToString() + " - Initial Prewarm: " + j.ToString());
        //        SpawnEmptySection(i, false);
        //    }
        //}

        // Probamos con la �ltima marea
        SpawnEmptySection(4, true);

        for (int j = 0; j < initialPrewarm; j++)
        {
            // Debug.Log("Seccion: " + i.ToString() + " - Initial Prewarm: " + j.ToString());
            SpawnEmptySection(4, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Instancia y posiciona cada una de las secciones
    /// </summary>
    [ContextMenu("SpawnSectionTest")]
    public void SpawnSection(int sectionNumber)
    {
        // Secci�n actual a instanciar
        ElementSection newSection = SelectNewElementSection(sectionNumber);

        // Si el tipo del elemento es roca o vacio, le asignaremos el layer ya que puede ser cualquiera
        if (newSection.elementType == ElementType.Rock || newSection.elementType == ElementType.Empty)
        {
            newSection.AssignLayer(sectionNumber);
        }

        // Vector para almacenar la desviaci�n a aplicar para situar la nueva plataforma
        Vector3 nextPositionOffset = Vector3.zero;

        // Sumamos las dos mitades de cada secci�n para colocar la nueva secci�n donde corresponde,
        // ya que pueden tener distinto tama�o
        nextPositionOffset.x = currentSections[sectionNumber].halfWidth + newSection.halfWidth;

        // Instancia un objeto a trav�s de un prefab, y la almacenamos como referencia la secci�n actual
        currentSections[sectionNumber] = Instantiate(newSection,
                                                        currentSections[sectionNumber].transform.position + nextPositionOffset,
                                                        Quaternion.identity,
                                                        sectionContainer
                                                    );
        // Cambiamos el nombre para evitar los (Clone)(Clone)...
        // sectionPrefabs[sectionNumber].name = "SeaSection" + sectionNumber.ToString();
    }

    /// <summary>
    /// Instancia y posiciona cada una de las secciones
    /// </summary>
    [ContextMenu("SpawnSectionTest")]
    public void SpawnEmptySection(int sectionNumber, bool isFirstElement)
    {
        // Secci�n actual a instanciar, a la que le asignamos el layer correspondiente
        ElementSection newSection = emptyElement;
        // Asigamos el layer directamente, ya que sabemos que es un objeto vacio
        emptyElement.AssignLayer(sectionNumber);

        // Vector para almacenar la desviaci�n a aplicar para situar la nueva plataforma
        Vector3 nextPositionOffset = Vector3.zero;

        // Sumamos las dos mitades de cada secci�n para colocar la nueva secci�n donde corresponde,
        // ya que pueden tener distinto tama�o
        nextPositionOffset.x = currentSections[sectionNumber].halfWidth + newSection.halfWidth;

        if (isFirstElement)
        {
            nextPositionOffset.y = initialPositionY[sectionNumber];
        }

        // Instancia un objeto a trav�s de un prefab, y la almacenamos como referencia la secci�n actual
        currentSections[sectionNumber] = Instantiate(newSection,
                                                        currentSections[sectionNumber].transform.position + nextPositionOffset,
                                                        Quaternion.identity,
                                                        sectionContainer
                                                    );
        // Cambiamos el nombre para evitar los (Clone)(Clone)...
        // sectionPrefabs[sectionNumber].name = "SeaSection" + sectionNumber.ToString();
    }

    /// <summary>
    /// Seleccionamos la nueva secci�n a generar. Pueden ser:
    /// - Una secci�n vacia, de tama�o 2, con una probabilidad del 65%
    /// - Una ola, de tama�o 8, con una probabilidad del 10%
    /// - Una roca, de tama�o 2, 3 o 4, con una probabilidad del 25%
    /// Todas estas probabilidades se pueden cambiar desde el editor. Esto lo har� el LevelManager
    /// </summary>
    /// <param name="sectionNumber"></param>
    public ElementSection SelectNewElementSection(int sectionNumber)
    {
        ElementSection[] waveSection;
        ElementSection section;

        // Lamzamos un rand para determinar cual ser� la siguiente secci�n a generar
        float rand = Random.Range( 0.0f, 1.0f );

        // Configuramos los l�mites para la tirada de datos del Random
        // Para la roca no es necesario ya que deber�a ser 1
        float emptyLimit = emptyProbability;
        float waveLimit = waveProbability + emptyProbability;

        // Elemento vacio
        if (rand >= 0f && rand < emptyProbability)
        {
            section = emptyElement;
        }
        // Ola
        else if (rand >= emptyLimit && rand < waveLimit)
        {
            switch (sectionNumber)
            {
                case 0:
                    waveSection = wave0Elements; break;
                case 1:
                    waveSection = wave1Elements; break;
                case 2:
                    waveSection = wave2Elements; break;
                case 3:
                    waveSection = wave3Elements; break;
                case 4:
                    waveSection = wave4Elements; break;
                default:
                    waveSection = wave0Elements; break;
            }

            section = waveSection[Random.Range(0, waveSection.Length)];
        }   
        // Roca
        else if (rand >= waveLimit && rand < 1f)
        {
            section = rockElements[Random.Range(0, rockElements.Length)];
        }
        // Para los dem�s casos, vacio
        else
        {
            section = emptyElement;
        } 

        return section;
    }
}
