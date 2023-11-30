using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Colliders : MonoBehaviour
{
    //lista de los objetos, luego accederemos a sus colliders para ir alternando
    public List<GameObject> listaDeMares;
    private int marActual = 0;
   
    
    void Start()
    {
        //desactivamos todos los colliders menos 1
       InitializeColliders();
    }

    void Update()
    {
       CambiodeMar();
        
        

    }

    void CambiodeMar()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            //estamos en el ultimo objeto de la lista?
            if (marActual < listaDeMares.Count - 1)
            {
                //si no estamos, incrementamos el número
                marActual++;
            }
            // Desactivamos el collider del objeto actual
            listaDeMares[marActual - 1].GetComponent<Collider2D>().enabled = false;

            // Activamos el collider del nuevo objeto al que vamos
            listaDeMares[marActual].GetComponent<Collider2D>().enabled = true;

        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            // Retrocedemos al objeto anterior si no estamos en el primero
            if (marActual > 0)
            {
                // Desactivamos el collider del objetoactual
                listaDeMares[marActual].GetComponent<Collider2D>().enabled = false;

                // restamos el numero de la lista
                marActual--;

                // Activamos el collider del nuevo objeto
                listaDeMares[marActual].GetComponent<Collider2D>().enabled = true;
            }
        }

    }
    void InitializeColliders()
    {
        // Desactivar los colliders de todos los objetos excepto el primero
        for (int i = 1; i < listaDeMares.Count; i++)
        {
            listaDeMares[i].GetComponent<Collider2D>().enabled = false;
        }
    }
}
