using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Velocidad del fondo, con 0.5 igualar�amos a la de la c�mara
    [Range(0f, 0.5f)]
    public float speedFactor = 0.066f;
    // Posici�n para control del offset de la textura
    private Vector2 pos = Vector2.zero;
    // Referencia al main camera
    private Camera cam;
    // Posicion anterior de la c�mara
    private Vector2 camOldPos;
    // Referencia al renderer del background para acceder a su material
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        // Inicializamos la posici�n anterior de la c�mara con la posici�n actual
        camOldPos = cam.transform.position;
        // Recuperamos la referencia al componente renderer
        rend = GetComponent<Renderer>();

        // ortographicSize es la mitad del alto de la c�mara
        // Screen.width es el ancho de la pantalla en p�xeles
        // Screen.height es el alto en p�xeles
        //  alturaOrtho ----- anchuraOrtho
        //       height ----- width
        // anchuraOrtho = (alturaOrtho * width) / height
        Vector2 backgroundHalfSize = new Vector2((cam.orthographicSize * Screen.width / Screen.height), cam.orthographicSize);

        // Ajustamos la escala del fondo para que se ajuste al tama�o de pantalla
        transform.localScale = new Vector3(backgroundHalfSize.x * 2,
                                           backgroundHalfSize.y * 2,
                                           1f);

        // Ajustamos el tilling para que sea proporcionado de forma correcta a la escala del quad
        // Lo dejamos a la mitad para reducir el n�mero de repeticiones
        rend.material.SetTextureScale("_MainTex", backgroundHalfSize);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculamos el desplazamiento de la c�mara respecto al ciclo anterior
        // No aplicamos variaci�n en y, luego lo mantenemos a 0
        Vector2 camVar = new Vector2(cam.transform.position.x - camOldPos.x, 0);

        // Modificamos el offset que se aplicar� a la textura
        // Lo multiplicamos por speedFactor para modificar su desplazamiento respecto a la c�mara
        pos.Set(pos.x + (camVar.x * speedFactor),
                pos.y + (camVar.y * speedFactor));

        // Aplicamos el offset a la textura principal
        rend.material.SetTextureOffset("_MainTex", pos);

        // Actualizamos posici�n de la c�mara para el siguiente ciclo
        camOldPos = cam.transform.position;
    }
}

