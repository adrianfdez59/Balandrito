using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Keys")]
    public KeyCode rotateKey = KeyCode.Space;
    public KeyCode switchForwardKey = KeyCode.W;
    public KeyCode switchBackwardKey = KeyCode.S;
    public KeyCode switchMoveLeftKey = KeyCode.A;
    public KeyCode switchMoveRightKey = KeyCode.D;
    public KeyCode switchRotateLeftKey = KeyCode.Q;
    public KeyCode switchRotateRightKey = KeyCode.E;

    [Header("Torque")]
    public float torque = 10f;
    public float maxTorque = 30f; // Límite máximo para el torque
    private Rigidbody2D rb;

    [Header("")]
    public Vector2 forceRight = new Vector2(250f, 0f);
    public Vector2 forceLeft = new Vector2(-250f, 0f);

    [Header("Others")]
    public PolygonCollider2D[] colliders;
    public BuoyancyEffector2D[] buoyancyEffectors;
    public Vector3[] positions;
    public float cooldown = 0.0f; // Tiempo de enfriamiento en segundos
    private int currentIndex = 0;
    private float lastPressTime;
    public bool canSwitchObject = true;
    private int currentLayer = 2;           // Empieza en el medio
    public SpriteRenderer spriteRenderer;

    public int[] capas;
    public int indiceCapaActual = 0;


    void Start()
    {
        spriteRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

        // Obtener el componente Rigidbody2D del objeto
        rb = GetComponent<Rigidbody2D>();

        // Verificar si se encontró el componente SpriteRenderer
        if (spriteRenderer == null)
        {
            enabled = false; // Desactivar el script si no se encuentra el componente SpriteRenderer
        }
        //DisableAllCollidersAndEffectors();
        //EnableColliderAndEffector(currentIndex);
        SetPosition(currentIndex);



    }

    void Update()
    {
        //if (Input.GetKeyDown(switchForwardKey))
        //{
        //    Debug.Log("W has been pressed. " + currentLayer.ToString() + " " + (colliders.Length - 1).ToString() + " - " + CanPress().ToString() + " - " + canSwitchObject.ToString());
        //}
        //if (Input.GetKeyDown(switchBackwardKey))
        //{
        //    Debug.Log("S has been pressed. " + currentLayer.ToString() + " " + (colliders.Length - 1).ToString() + " - " + CanPress().ToString() + " - " + canSwitchObject.ToString());
        //}

        if (Input.GetKeyDown(switchForwardKey) && currentLayer < colliders.Length - 1 && CanPress() && canSwitchObject)
        {
            currentLayer += 1;
            ChangeLayer(currentLayer);

            lastPressTime = Time.time;

            //Debug.Log("Previous Layer: " + currentLayer.ToString() + " - New Layer: " + (currentLayer+1).ToString());


        }
        else if (Input.GetKeyDown(switchBackwardKey) && currentLayer > 0 && CanPress() && canSwitchObject)
        {
            currentLayer -= 1;
            ChangeLayer(currentLayer);

            lastPressTime = Time.time;

            //Debug.Log("Previous Layer: " + currentLayer.ToString() + " - New Layer: " + (currentLayer - 1).ToString());


        }

        // Desplazamos el barco izquierda y derecha, con un poco de torque
        if(Input.GetKeyDown(switchMoveLeftKey))
        {
            rb.AddForce(forceLeft);

            float torqueToApply = Mathf.Clamp(-1.0f * torque, -maxTorque, maxTorque);
            rb.AddTorque(torqueToApply, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(switchMoveRightKey))
        {
            rb.AddForce(forceRight);

            float torqueToApply = Mathf.Clamp(1.0f * torque, -maxTorque, maxTorque);
            rb.AddTorque(torqueToApply, ForceMode2D.Impulse);
        }

        // Rotamos el barco hacia adelante y hacia atrás
        if (Input.GetKeyDown(switchRotateLeftKey))
        {
            float torqueToApply = Mathf.Clamp(-1.0f * torque, -maxTorque, maxTorque);

            rb.AddTorque(torqueToApply, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(switchRotateRightKey))
        {
            float torqueToApply = Mathf.Clamp(1.0f * torque, -maxTorque, maxTorque);

            rb.AddTorque(torqueToApply, ForceMode2D.Impulse);
        }

        // Realizamos un flip del barco
        if (Input.GetKeyDown(rotateKey))
        {
            Rotate180Degrees();
        }
    }

    //void LayerSwitch()
    //{
    //    if (currentOrder == 5)
    //    {
    //        gameObject.layer = 10;
    //    }
    //    if (currentOrder == 4)
    //    {
    //        gameObject.layer = 9;
    //    }
    //    if (currentOrder == 3)
    //    {
    //        gameObject.layer = 8;
    //    }
    //    if (currentOrder == 2)
    //    {
    //        gameObject.layer = 7;
    //    }
    //    if (currentOrder == 1)
    //    {
    //        gameObject.layer = 6;
    //    }
    //}

    /// <summary>
    /// Se traslada el objeto al layer correspondiente
    /// </summary>
    /// <param name="sectionNumber"></param>
    public void ChangeLayer(int sectionNumber)
    {
        switch (sectionNumber)
        {
            case 0:
                gameObject.layer = LayerMask.NameToLayer("Section0");
                gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section0");
                SetPosition(0);
                break;
            case 1:
                gameObject.layer = LayerMask.NameToLayer("Section1");
                gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section1");
                SetPosition(1);
                break;
            case 2:
                gameObject.layer = LayerMask.NameToLayer("Section2");
                gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section2");
                SetPosition(2);
                break;
            case 3:
                gameObject.layer = LayerMask.NameToLayer("Section3");
                gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section3");
                SetPosition(3);
                break;
            case 4:
                gameObject.layer = LayerMask.NameToLayer("Section4");
                gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section4");
                SetPosition(4);
                break;
            default:
                break;
        }

        //Debug.Log("Current Layer: " + gameObject.layer);
    }




    //void SwitchObject(int orderChange)
    //{
    //    // Cambiar el Order in Layer
    //    currentOrder = Mathf.Clamp(currentOrder + orderChange, 0, 4);
    //    spriteRenderer.sortingOrder = currentOrder;

    //    // Registrar el tiempo de la última pulsación
    //    lastPressTime = Time.time;
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("sea"))
        //{
        //    canSwitchObject = true;
        //}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if (other.CompareTag("sea"))
        //{
        //    canSwitchObject = false;
        //}
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
        //DisableColliderAndEffector(currentIndex);
        SetPosition(currentIndex);

        if (forward)
        {
            currentIndex = Mathf.Min(currentIndex + 1, colliders.Length - 1);
        }
        else
        {
            currentIndex = Mathf.Max(currentIndex - 1, 0);
        }

        //EnableColliderAndEffector(currentIndex);
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
