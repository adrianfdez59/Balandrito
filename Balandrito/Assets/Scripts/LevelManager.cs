using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Controla las velocidades de cada carril (marea + elementos)
    // La primera posición es la marea más cercana, y la última la más lejana
    public float[] velocityXSections;

    // PATRÓN SINGLETON
    // Creamos una variable pública y estática
    public static LevelManager instance;


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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
