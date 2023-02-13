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
    public List<Interactable> currentInteractable = new List<Interactable>();
    [HideInInspector]
    public GameObject carry;
    private Pausable _pausable;

    private void Awake()
    {
        _pausable = GetComponent<Pausable>();
    }

    private void Update()
    {
        if (_pausable.Paused)
        {
            return;
        }
        
        if (Input.GetButtonDown("Cancel"))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (currentInteractable.Count == 0 && carry)
            {
                Item.spawnItem(carry, 10F);
                Destroy(carry);
            }
            
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