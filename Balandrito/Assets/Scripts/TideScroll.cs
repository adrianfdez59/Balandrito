using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideScroll : MonoBehaviour
{
    // Array con cada una de las velocidades que tendrán las miniolas
    // Estos valores irán variando por tiempo, de forma random; y por escena
    // Los valores positivos indicarán que las olas se mueven a la derecha, y
    // los valores negativos que se mueven a la izquierda
    [Range(-0.1f, 0.1f)]
    public float tideScrollVelocityX;
    // Desplazamiento de la miniola en Y
    public float tideMoveY;

    private Vector3 pos = Vector3.zero;

    // Referencia al MeshRenderer del mar para acceder a su material
    private SpriteRenderer meshRend;

    // Start is called before the first frame update
    void Start()
    {
        // Recuperamos la referencia al componente Renderer
        meshRend = GetComponent<SpriteRenderer>();

        // pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Desplazamos el offset de la miniola
        meshRend.material.mainTextureOffset += new Vector2(tideScrollVelocityX * Time.deltaTime, 0);

        // Desplazamos en Y la miniola, siguiendo un movimiento sinusoidal
        // pos.y += tideMoveY * Mathf.Sin(Time.deltaTime);
        // pos.y += Mathf.Sin(Time.deltaTime);

        // transform.Translate(pos.x, pos.y, pos.z);
    }
}
