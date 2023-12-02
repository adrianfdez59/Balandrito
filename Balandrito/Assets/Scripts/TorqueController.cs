using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueController : MonoBehaviour
{
    public float torque = 10f;
    public float maxTorque = 30f; // Límite máximo para el torque

    public Rigidbody2D rb;

    void Start()
    {
        // Obtener el componente Rigidbody2D del objeto
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float inputHorizontal = Input.GetAxis("Horizontal");

        
        float torqueToApply = Mathf.Clamp(-inputHorizontal * torque, -maxTorque, maxTorque);

      
        rb.AddTorque(torqueToApply, ForceMode2D.Impulse);
    }
}
