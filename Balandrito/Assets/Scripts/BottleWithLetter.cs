using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class BottleWithLetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] rock = GameObject.FindGameObjectsWithTag("Rock");

        if (rock != null)
        {
            foreach (var rockItem in rock)
            {
                Physics2D.IgnoreCollision(rockItem.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }

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
                break;
            case 1:
                gameObject.layer = LayerMask.NameToLayer("Section1");
                gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section1");
                break;
            case 2:
                gameObject.layer = LayerMask.NameToLayer("Section2");
                gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section2");
                break;
            case 3:
                gameObject.layer = LayerMask.NameToLayer("Section3");
                gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section3");
                break;
            case 4:
                gameObject.layer = LayerMask.NameToLayer("Section4");
                gameObject.GetComponentInChildren<Renderer>().sortingLayerID = SortingLayer.NameToID("Section4");
                break;
            default:
                break;
        }
    }
}
