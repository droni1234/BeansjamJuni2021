using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private GameObject currentInteractable;
    private GameObject carry;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!currentInteractable)
            {
                return;
            }

            var interactable = currentInteractable.GetComponent<Interactable>();
            if (!interactable)
            {
                return;
            }

            var surgeryTable = currentInteractable.GetComponent<SurgeryTable>();
            if (interactable.pickup)
            {
                DestroyChildren();

                carry = Instantiate(
                    interactable.pickup,
                    transform.position,
                    interactable.pickup.transform.rotation,
                    transform
                );

                carry.transform.localScale = interactable.pickup.transform.localScale;
            }
            else if (surgeryTable)
            {
                var bodyPart = carry.GetComponent<BodyPart>();
                if (!bodyPart)
                {
                    return;
                }
                
                surgeryTable.AddPart(bodyPart);
                DestroyChildren();
                carry = null;
            }
        }
    }

    private void DestroyChildren()
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        currentInteractable = other.gameObject;
    }
}