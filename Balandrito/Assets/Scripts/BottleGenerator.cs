using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGenerator : MonoBehaviour
{
    // Lugar desde el que se van soltando las botellas. Se hará fuera de cámara
    public Transform dropPoint;

    // Referencia al prefab de la botella
    public BottleWithLetter bottle;

    // Tiempo que tarda en generar la primera botella, y el intervalo
    public float spawnDelay;
    public float spawnIntervalBegin;
    public float spawnIntervalEnd;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnBottleAtRandomInterval", spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBottleAtRandomInterval()
    {
        // Decidimos el espacio de tiempo sobre el que se eligirá cuando aparece la siguiente botella
        float time = Random.Range(spawnIntervalBegin, spawnIntervalEnd);

        SpawnBottle();

        // Seguiremos instanciando botellas hasta que se recojan todas las botellas del nivel
        // if (BottlePicked < objective)
        Invoke("SpawnBottleAtRandomInterval", time);
    }

    /// <summary>
    /// Instancia una botella en el lugar indicado
    /// </summary>
    private void SpawnBottle()
    {
        // Elegimos en qué layer ira la botella
        int section = Random.Range(0, 5);
        bottle.ChangeLayer(section);

        // Instanciamos la botella en el dropPoint
        Instantiate(bottle, dropPoint);
    }
}
