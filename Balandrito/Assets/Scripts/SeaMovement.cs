using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMovement : MonoBehaviour
{
    // Array con cada una de las velocidades que tendrán las olas
    // Estos valores irán variando por tiempo, de forma random; y por escena
    // Los valores positivos indicarán que las olas se mueven a la derecha, y
    // los valores negativos que se mueven a la izquierda
    public int seaWaveVelocity;

    // Distancia entre cada ola
    public float distanceBetweenSeaWave = 0.1f;

    // Offset de la primera ola con respecto a la esquina inferior izquierda
    public float offsetFirstSeaWave = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
