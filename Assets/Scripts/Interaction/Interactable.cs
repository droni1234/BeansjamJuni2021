using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    private Interactor holdingPlayer;
    
    public abstract void Trigger(Interactor interactor);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            holdingPlayer = other.GetComponent<Interactor>();
            other.GetComponent<Interactor>()?.currentInteractable.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            holdingPlayer?.currentInteractable.Remove(this);
            other.GetComponent<Interactor>()?.currentInteractable.Remove(this);
    }

    private void OnDestroy()
    {
        holdingPlayer?.currentInteractable.Remove(this);
    }
    
}