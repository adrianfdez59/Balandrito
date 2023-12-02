using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewController : MonoBehaviour
{
    public KeyCode rotateKey = KeyCode.Space;
  
    public KeyCode switchForwardKey = KeyCode.W;
    public KeyCode switchBackwardKey = KeyCode.S;
    public PolygonCollider2D[] colliders;
    public BuoyancyEffector2D[] buoyancyEffectors;
    public Vector3[] positions;
    private int currentIndex = 0;

    void Start()
    {
        // Desactivar todos los colliders, effectors y establecer la posición del primero
        DisableAllCollidersAndEffectors();
        EnableColliderAndEffector(currentIndex);
        SetPosition(currentIndex);
    }

    void Update()
    {

        // Rotar 180 grados al presionar la tecla espacio
        if (Input.GetKeyDown(rotateKey))
        {
            Rotate180Degrees();
        }
        // Cambiar al collider, effector y posición hacia adelante al pulsar la tecla 'W'
        if (Input.GetKeyDown(switchForwardKey) && currentIndex < colliders.Length - 1)
        {
            SwitchObject(true);
        }
        // Cambiar al collider, effector y posición hacia atrás al pulsar la tecla 'S'
        else if (Input.GetKeyDown(switchBackwardKey) && currentIndex > 0)
        {
            SwitchObject(false);
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
        // Desactivar el collider, effector actual y establecer la posición actual
        DisableColliderAndEffector(currentIndex);
        SetPosition(currentIndex);

        // Calcular el nuevo índice
        if (forward)
        {
            currentIndex = Mathf.Min(currentIndex + 1, colliders.Length - 1);
        }
        else
        {
            currentIndex = Mathf.Max(currentIndex - 1, 0);
        }

        // Activar el nuevo collider, effector y establecer la nueva posición
        EnableColliderAndEffector(currentIndex);
        SetPosition(currentIndex);
    }

    //por si se queda estancado, si pulsa espacio rota 180 grados
    void DisableColliderAndEffector(int index)
    {
        colliders[index].enabled = false;
        buoyancyEffectors[index].enabled = false;
    }
    void Rotate180Degrees()
    {
        
        Vector3 currentRotation = transform.eulerAngles;

        // Rotar 180 grados alrededor del eje Y
        currentRotation.z += 180f;

        
        transform.eulerAngles = currentRotation;
    }
}
