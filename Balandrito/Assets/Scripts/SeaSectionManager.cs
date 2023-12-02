using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSectionManager : MonoBehaviour
{
    // Array de secciones disponibles
    public SeaSection[] sectionPrefabs;
    // Transform que contendr� las secciones generadas
    public Transform sectionContainer;
    // �ltima secci�n generada
    // public SeaSection currentSection;
    //N�mero de plataformas generadas inicialmente
    public int initialPrewarm = 2;

    // PATR�N SINGLETON
    // Creamos una variable p�blica y est�tica
    public static SeaSectionManager instance;


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
    /// Instancia y posiciona una nueva secci�n
    /// </summary>
    //[ContextMenu("SpawnSectionTest")]
    //public void SpawnSection()
    //{
    //    // Obtenemos una nueva secci�n del array de forma aleatoria
    //    SeaSection newSection = sectionPrefabs[Random.Range(0, sectionPrefabs.Length)];

    //    // Vector para almacenar la desviaci�n a aplicar para situar la nueva plataforma
    //    Vector3 nextPositionOffset = Vector3.zero;
    //    // Sumamos las dos mitades de cada secci�n para colocar la nueva secci�n donde corresponde,
    //    // ya que pueden tener distinto tama�o
    //    nextPositionOffset.x = currentSection.halfWidth + newSection.halfWidth;

    //    // Instancia un objeto a trav�s de un prefab, y la almacenamos como referencia la secci�n actual
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
        // Secci�n actual a instanciar
        SeaSection newSection = sectionPrefabs[sectionNumber];

        // Vector para almacenar la desviaci�n a aplicar para situar la nueva plataforma
        Vector3 nextPositionOffset = Vector3.zero;

        // Sumamos las dos mitades de cada secci�n para colocar la nueva secci�n donde corresponde,
        // ya que pueden tener distinto tama�o
        nextPositionOffset.x = sectionPrefabs[sectionNumber].halfWidth + newSection.halfWidth;

        // Instancia un objeto a trav�s de un prefab, y la almacenamos como referencia la secci�n actual
        sectionPrefabs[sectionNumber] = Instantiate(   newSection,
                                                        sectionPrefabs[sectionNumber].transform.position + nextPositionOffset,
                                                        Quaternion.identity,
                                                        sectionContainer
                                                    );
        // Cambiamos el nombre para evitar los (Clone)(Clone)...
        sectionPrefabs[sectionNumber].name = "SeaSection" + sectionNumber.ToString();
    }
}
