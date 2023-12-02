using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullAttack : MonoBehaviour
{

    public float raycastLenght = 100f;
    public float speed = 20f;
    public bool attack;

    private void Update()
    {
        //dirección raycast
        Vector2 directionRaycast = new Vector2(-1f, -1f).normalized;

        // Lanzamos el raycast abajo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionRaycast, raycastLenght);

        // Esto para ver en tiempo real el raycast
        Debug.DrawRay(transform.position, directionRaycast * raycastLenght, Color.red);

        // si el raycast hace hit con el barquito
        if (hit.collider.CompareTag("Player"))
        {
            //accion atacar se activa
            attack = true;
        }
        //si atacar se activa hacemos el desplazamiento de la gaviota
        if (attack == true)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 64f);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
        

    
    

    

