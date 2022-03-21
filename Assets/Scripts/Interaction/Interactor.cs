using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Pausable))]
public class Interactor : MonoBehaviour
{
    public float actionProgressPerSecond = 1.0f;
    public Transform pickupPosition;
    public Slider progressSlider;
    public GameObject ratDead;

    [HideInInspector]
    public List<Interactable> currentInteractable;
    //[HideInInspector]
    public GameObject carry;

    void Update()
    {
        if (GetComponent<Pausable>().Paused)
        {
            return;
        }
        
        if (Input.GetButtonDown("Cancel"))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Interactable interactable in currentInteractable.ToList())
            {
                interactable.Trigger(this);
            }

        }
    }

    public void DestroyCarriedBodyPart()
    {
        foreach (BodyPart bodyPart in GetComponentsInChildren<BodyPart>())
        {
            Destroy(bodyPart.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Interactable>())
            currentInteractable.Add(other.GetComponent<Interactable>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<Interactable>())
            currentInteractable.Remove(other.GetComponent<Interactable>());
    }

}