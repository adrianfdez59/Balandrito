using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayer : MonoBehaviour
{
    public KeyCode decreaseOrderKey = KeyCode.W;
    public KeyCode increaseOrderKey = KeyCode.S;
    private SpriteRenderer spriteRenderer;
    private int currentOrder = 5; // Inicia en 5, según tu descripción

    void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Verificar si se encontró el componente SpriteRenderer
        if (spriteRenderer == null)
        {
            
            enabled = false; // Desactivar el script si no se encuentra el componente SpriteRenderer
        }
    }

    void Update()
    {
        //  tecla 'W'
        if (Input.GetKeyDown(decreaseOrderKey) && currentOrder > 0)
        {
            ChangeOrderInLayer(-1);
        }

        // tecla 'S'
        if (Input.GetKeyDown(increaseOrderKey) && currentOrder < 5)
        {
            ChangeOrderInLayer(1);
        }
    }

    void ChangeOrderInLayer(int orderChange)
    {
        // Cambiar el Order in Layer
        currentOrder = Mathf.Clamp(currentOrder + orderChange, 0, 5);
        spriteRenderer.sortingOrder = currentOrder;
    }
}
