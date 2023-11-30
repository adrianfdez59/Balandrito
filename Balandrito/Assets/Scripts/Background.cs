using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Referencia al main camera
    private Camera cam;
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
