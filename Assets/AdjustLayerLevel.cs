using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AdjustLayerLevel : MonoBehaviour
{

    void Update()
    {
        foreach (Renderer sr in FindObjectsOfType<Renderer>())
        {
            if(sr.sortingLayerID == SortingLayer.GetLayerValueFromName("Default"))
                sr.sortingOrder = (int)(-sr.transform.position.y * 100);
        }
    }
}
