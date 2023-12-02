using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSectionManager : MonoBehaviour
{
    // Array de secciones disponibles
    public SeaSection[] sectionPrefabs;
    // Transform que contendrá las secciones generadas
    public Transform sectionContainer;
    // Última sección generada
    // public SeaSection currentSection;
    //Número de plataformas generadas inicialmente
    public int initialPrewarm = 2;

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

        // Crearemos 5 secciones por cada initialPrewarm marcado
        for (int j = 0; j < initialPrewarm; j++)
        {
            for (int i = 0; i < sectionPrefabs.Length; i++)
            {
                // Debug.Log("Seccion: " + i.ToString() + " - Initial Prewarm: " + j.ToString());
                SpawnSection(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Instancia y posiciona una nueva sección
    /// </summary>
    //[ContextMenu("SpawnSectionTest")]
    //public void SpawnSection()
    //{
    //    // Obtenemos una nueva sección del array de forma aleatoria
    //    SeaSection newSection = sectionPrefabs[Random.Range(0, sectionPrefabs.Length)];

    //    // Vector para almacenar la desviación a aplicar para situar la nueva plataforma
    //    Vector3 nextPositionOffset = Vector3.zero;
    //    // Sumamos las dos mitades de cada sección para colocar la nueva sección donde corresponde,
    //    // ya que pueden tener distinto tamaño
    //    nextPositionOffset.x = currentSection.halfWidth + newSection.halfWidth;

    //    // Instancia un objeto a través de un prefab, y la almacenamos como referencia la sección actual
    //    currentSection = Instantiate(newSection,
    //                                    currentSection.transform.position + nextPositionOffset,
    //                                    Quaternion.identity,
    //                                    sectionContainer);
    //}

    /// <summary>
    /// Instancia y posiciona cada una de las secciones
    /// </summary>
    [ContextMenu("SpawnSectionTest")]
    public void SpawnSection(int sectionNumber)
    {
        // Sección actual a instanciar
        SeaSection newSection = sectionPrefabs[sectionNumber];

        // Vector para almacenar la desviación a aplicar para situar la nueva plataforma
        Vector3 nextPositionOffset = Vector3.zero;

        // Sumamos las dos mitades de cada sección para colocar la nueva sección donde corresponde,
        // ya que pueden tener distinto tamaño
        nextPositionOffset.x = sectionPrefabs[sectionNumber].halfWidth + newSection.halfWidth;

        // Instancia un objeto a través de un prefab, y la almacenamos como referencia la sección actual
        sectionPrefabs[sectionNumber] = Instantiate(   newSection,
                                                        sectionPrefabs[sectionNumber].transform.position + nextPositionOffset,
                                                        Quaternion.identity,
                                                        sectionContainer
                                                    );
        // Cambiamos el nombre para evitar los (Clone)(Clone)...
        sectionPrefabs[sectionNumber].name = "SeaSection" + sectionNumber.ToString();
    }
}
