using System;
using UnityEngine;
    
    [RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    private Interactor holdingPlayer;

    public virtual void Trigger(Interactor interactor)
    {
        
    }

    public virtual void OnDestroy()
    {
        holdingPlayer?.currentInteractable.Remove(this);
    }
    
}