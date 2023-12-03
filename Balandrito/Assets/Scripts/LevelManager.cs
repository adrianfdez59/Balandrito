using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Controla las velocidades de cada carril (marea + elementos)
    // La primera posici�n es la marea m�s cercana, y la �ltima la m�s lejana
    public float[] velocityXSections;

    // PATR�N SINGLETON
    // Creamos una variable p�blica y est�tica
    public static LevelManager instance;


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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
