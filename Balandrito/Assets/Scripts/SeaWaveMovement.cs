using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWaveMovement : MonoBehaviour
{
    // Array con cada una de las velocidades que tendrán las olas
    // Estos valores irán variando por tiempo, de forma random; y por escena
    // Los valores positivos indicarán que las olas se mueven a la derecha, y
    // los valores negativos que se mueven a la izquierda
    [Range(-2f, 2f)]
    public float[] seaWaveVelocity;
    // Distancia entre cada ola
    public float distanceBetweenSeaWave = 0.1f;
    // Offset de la primera ola con respecto a la esquina inferior izquierda
    public float offsetFirstSeaWave = 0;
    // Posición para control del offset de la textura
    private Vector2 pos = Vector2.zero;

    // Referencia al main camera
    private Camera cam;
    // Posicion anterior de la cámara
        // private Vector2 camOldPos;
    // Referencia al renderer del background para acceder a su material
    private Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;


        // Recuperamos la referencia al componente renderer
        rend = GetComponent<Renderer>();

        // ortographicSize es la mitad del alto de la cámara
        // Screen.width es el ancho de la pantalla en píxeles
        // Screen.height es el alto en píxeles
        //  alturaOrtho ----- anchuraOrtho
        //       height ----- width
        // anchuraOrtho = (alturaOrtho * width) / height
        Vector2 backgroundHalfSize = new Vector2((cam.orthographicSize * Screen.width / Screen.height), cam.orthographicSize);

        // Ajustamos la escala del fondo para que se ajuste al tamaño de pantalla
        transform.localScale = new Vector3(backgroundHalfSize.x * 2,
                                           backgroundHalfSize.y * 2,
                                           1f);

        // Ajustamos el tilling para que sea proporcionado de forma correcta a la escala del quad
        // Lo dejamos a la mitad para reducir el número de repeticiones
        rend.material.SetTextureScale("_MainTex", backgroundHalfSize);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
