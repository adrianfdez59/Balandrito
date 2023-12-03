using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeaSectionManager : MonoBehaviour
{
    // Array de secciones disponibles (uno por cada sección)
    // Se incluye también un array de enteros con las ocurrencias de cada 
    // Se podrán configurar desde el LevelManager para que la variación de las ocurrencias afecten
    // a la dificultad del nivel
    [Header("Sections and Ocurrences")]
    public SeaSection[] section0Prefabs;
    public int[] section0Occurences;
    public SeaSection[] section1Prefabs;
    public int[] section1Occurences;
    public SeaSection[] section2Prefabs;
    public int[] section2Occurences;
    public SeaSection[] section3Prefabs;
    public int[] section3Occurences;
    public SeaSection[] section4Prefabs;
    public int[] section4Occurences;

    // Array de Listas que controla cuantas veces ha aparecido una sección
    // Hasta que todas las ocurrencias de una misma sección no llegan a 0, no se vuelven
    // a resetear. Una vez se extrae una sección al azar, se le resta 1 a la ocurrencia.
    // Con esto controlamos la dificultad del nivel 
    private List<int>[] currentOcurrences = new List<int>[5];

    [Header("Initialization")]
    // Transform que contendrá las secciones generadas
    public Transform sectionContainer;
    // Última sección generada
    // public SeaSection currentSection;
    //Número de plataformas generadas inicialmente
    public int initialPrewarm = 2;

    // Array de secciones actuales
    public SeaSection[] currentSections;

    // PATRÓN SINGLETON
    // Creamos una variable pública y estática
    public static SeaSectionManager instance;


    private void Awake()
    {
        // Verificamos is la instancia es nula. De ser así significa que no se ha creado antes
        // y que esta es la primera
        if (instance == null)
        {
            // Hacemos que esta instancia quede referida como estática, pudiendo acceder a ella sin referencia alguna
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Si no tenemos un container, lo asignamos
        if (!sectionContainer) 
            sectionContainer = transform;

        //Crearemos 5 secciones por cada initialPrewarm marcado
        for (int j = 0; j < initialPrewarm; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                // Debug.Log("Seccion: " + i.ToString() + " - Initial Prewarm: " + j.ToString());
                SpawnEmptySection(i);
            }
        }

        // Inicializamos el array de listas
        for (int i = 0; i < 5; i++)
        {
            currentOcurrences[i] = new List<int>();
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
        // Sección actual a instanciar
        SeaSection[] sections;
        SeaSection newSection;

        switch (sectionNumber)
        {
            case 0:
                sections = section0Prefabs; break;
            case 1:
                sections = section1Prefabs; break;
            case 2:
                sections = section2Prefabs; break;
            case 3:
                sections = section3Prefabs; break;
            case 4:
                sections = section4Prefabs; break;
            default:
                sections = section0Prefabs; break; // No debería entrar aquí
        }

        // Si la lista está vacía, la llenamos con el batch de secciones a escoger, ya reordenadas
        if (currentOcurrences[sectionNumber].Count == 0)
        {
            currentOcurrences[sectionNumber] = GenerateAndShuffleIndexSections(sectionNumber);
        }

        // Cogemos el primer elemento de las ocurrencias y lo eliminamos
        newSection = sections[currentOcurrences[sectionNumber].First()];
        currentOcurrences[sectionNumber].RemoveAt(0);

        // Vector para almacenar la desviación a aplicar para situar la nueva plataforma
        Vector3 nextPositionOffset = Vector3.zero;

        // Sumamos las dos mitades de cada sección para colocar la nueva sección donde corresponde,
        // ya que pueden tener distinto tamaño
        nextPositionOffset.x = currentSections[sectionNumber].halfWidth + newSection.halfWidth;

        // Instancia un objeto a través de un prefab, y la almacenamos como referencia la sección actual
        currentSections[sectionNumber] = Instantiate(   newSection,
                                                        currentSections[sectionNumber].transform.position + nextPositionOffset,
                                                        Quaternion.identity,
                                                        sectionContainer
                                                    );
        // Cambiamos el nombre para evitar los (Clone)(Clone)...
        currentSections[sectionNumber].name = "SeaSection" + sectionNumber.ToString();
    }

    /// <summary>
    /// Instancia y posiciona cada una de las secciones, siendo estas vacias, sin obstáculos
    /// Esta instancia será la primera en el array
    /// </summary>
    [ContextMenu("SpawnSectionTest")]
    public void SpawnEmptySection(int sectionNumber)
    {
        // Sección actual a instanciar
        SeaSection newSection;

        switch (sectionNumber)
        {
            case 0:
                newSection = section0Prefabs[0]; break;
            case 1:
                newSection = section1Prefabs[0]; break;
            case 2:
                newSection = section2Prefabs[0]; break;
            case 3:
                newSection = section3Prefabs[0]; break;
            case 4:
                newSection = section4Prefabs[0]; break;
            default:
                newSection = section0Prefabs[0]; break; // No debería entrar aquí
        }

        // Vector para almacenar la desviación a aplicar para situar la nueva plataforma
        Vector3 nextPositionOffset = Vector3.zero;

        // Sumamos las dos mitades de cada sección para colocar la nueva sección donde corresponde,
        // ya que pueden tener distinto tamaño
        nextPositionOffset.x = currentSections[sectionNumber].halfWidth + newSection.halfWidth;

        // Instancia un objeto a través de un prefab, y la almacenamos como referencia la sección actual
        currentSections[sectionNumber] = Instantiate(newSection,
                                                        currentSections[sectionNumber].transform.position + nextPositionOffset,
                                                        Quaternion.identity,
                                                        sectionContainer
                                                    );
        // Cambiamos el nombre para evitar los (Clone)(Clone)...
        currentSections[sectionNumber].name = "SeaSection" + sectionNumber.ToString();
    }

    // Genera y reeordena todas las secciones a generar 
    private List<int> GenerateAndShuffleIndexSections(int sectionNumber)
    {
        // Occurencias de la sección asociada al carril pasado como argumento
        int[] sectionOccurences;
        // Lista con los indices 
        List<int> indexSections = new List<int>();

        switch (sectionNumber)
        {
            case 0:
                sectionOccurences = section0Occurences; break;
            case 1:
                sectionOccurences = section1Occurences; break;
            case 2:
                sectionOccurences = section2Occurences; break;
            case 3:
                sectionOccurences = section3Occurences; break;
            case 4:
                sectionOccurences = section4Occurences; break;
            default:
                sectionOccurences = section0Occurences; break; // No debería entrar aquí
        }

        

        for (int i = 0; i < sectionOccurences.Length; i++)
        {
            for (int j = 0; j < sectionOccurences[i]; j++)
            {
                indexSections.Add(i);
            }
        }

        // Realizamos un shuffle de los índices
        int n = indexSections.Count;
        System.Random rng = new System.Random();

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = indexSections[k];
            indexSections[k] = indexSections[n];
            indexSections[n] = value;
        }

        return indexSections;
    }
}
