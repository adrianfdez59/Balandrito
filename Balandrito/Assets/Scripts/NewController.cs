using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NewController : MonoBehaviour
{
    public KeyCode rotateKey = KeyCode.Space;
    public KeyCode switchForwardKey = KeyCode.W;
    public KeyCode switchBackwardKey = KeyCode.S;
    public PolygonCollider2D[] colliders;
    public BuoyancyEffector2D[] buoyancyEffectors;
    public Vector3[] positions;
    public float cooldown = 0.5f; // Tiempo de enfriamiento en segundos
    private int currentIndex = 0;
    private float lastPressTime;
    public bool canSwitchObject = false;
    private int currentOrder = 8;
    public SpriteRenderer spriteRenderer;

    public int[] capas;
    public int indiceCapaActual = 0;

    void Start()
    {

        spriteRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

        // Verificar si se encontró el componente SpriteRenderer
        if (spriteRenderer == null)
        {
            enabled = false; // Desactivar el script si no se encuentra el componente SpriteRenderer
        }
        DisableAllCollidersAndEffectors();
        EnableColliderAndEffector(currentIndex);
        SetPosition(currentIndex);

        

    }

    void Update()
    {
        if (Input.GetKeyDown(switchForwardKey) && currentIndex < colliders.Length - 1 && CanPress() && canSwitchObject)
        {
            SwitchObject(true);
            SwitchObject(-2);
            LayerSwitch();
            

        }
        else if (Input.GetKeyDown(switchBackwardKey) && currentIndex > 0 && CanPress() && canSwitchObject)
        {
            SwitchObject(false);
            SwitchObject(2);
            LayerSwitch();


        }

        if (Input.GetKeyDown(rotateKey))
        {
            Rotate180Degrees();
        }
    }

    void LayerSwitch()
    {
        if (currentOrder == 8)
        {
            gameObject.layer = 10;
        }
        if (currentOrder == 6)
        {
            gameObject.layer = 9;
        }
        if (currentOrder == 4)
        {
            gameObject.layer = 8;
        }
        if (currentOrder == 2)
        {
            gameObject.layer = 7;
        }
        if (currentOrder == 0)
        {
            gameObject.layer = 6;
        }
    }

   

    void SwitchObject(int orderChange)
    {
        // Cambiar el Order in Layer
        currentOrder = Mathf.Clamp(currentOrder + orderChange, 0, 9);
        spriteRenderer.sortingOrder = currentOrder;

        // Registrar el tiempo de la última pulsación
        lastPressTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("sea"))
        {
            canSwitchObject = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("sea"))
        {
            canSwitchObject = false;
        }
    }

    void DisableAllCollidersAndEffectors()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
            buoyancyEffectors[i].enabled = false;
        }
    }

    void EnableColliderAndEffector(int index)
    {
        colliders[index].enabled = true;
        buoyancyEffectors[index].enabled = true;
    }

    void SetPosition(int index)
    {
        if (index < positions.Length)
        {
            transform.position = new Vector3(transform.position.x, positions[index].y, positions[index].z);
        }
    }

    void SwitchObject(bool forward)
    {
        DisableColliderAndEffector(currentIndex);
        SetPosition(currentIndex);

        if (forward)
        {
            currentIndex = Mathf.Min(currentIndex + 1, colliders.Length - 1);
        }
        else
        {
            currentIndex = Mathf.Max(currentIndex - 1, 0);
        }

        EnableColliderAndEffector(currentIndex);
        SetPosition(currentIndex);

        lastPressTime = Time.time;
    }

    void DisableColliderAndEffector(int index)
    {
        colliders[index].enabled = false;
        buoyancyEffectors[index].enabled = false;
    }

    void Rotate180Degrees()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z += 180f;
        transform.eulerAngles = currentRotation;
    }

    bool CanPress()
    {
        return Time.time - lastPressTime >= cooldown;
    }
}
