using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Jobs;

public class SeaWaveScroll : MonoBehaviour
{
    // Array con cada una de las velocidades que tendrán las miniolas
    // Estos valores irán variando por tiempo, de forma random; y por escena
    // Los valores positivos indicarán que las olas se mueven a la derecha, y
    // los valores negativos que se mueven a la izquierda
    [Range(-0.1f, 0.1f)]
    public float seaWaveScrollVelocityX;
    // Desplazamiento de la miniola en Y
    public float seaWaveMoveY;

    private Vector3 pos = Vector3.zero;

    // Referencia al MeshRenderer del mar para acceder a su material
    private MeshRenderer meshRend;

    // Start is called before the first frame update
    void Start()
    {
        // Recuperamos la referencia al componente Renderer
        meshRend = GetComponent<MeshRenderer>();

        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Desplazamos el offset de la miniola
        meshRend.material.mainTextureOffset += new Vector2( seaWaveScrollVelocityX * Time.deltaTime, 0 );

        // Desplazamos en Y la miniola, siguiendo un movimiento sinusoidal
        pos.y += Mathf.Sin(seaWaveMoveY * Time.deltaTime);

        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }
}
