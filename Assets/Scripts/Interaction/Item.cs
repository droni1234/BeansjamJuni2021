using UnityEngine;


public class Item : Interactable
{
    public GameObject pickup;

    public override void Trigger(Interactor interactor)
    {
        if (!pickup) 
            return;
        
        interactor.DestroyCarriedBodyPart();

        interactor.carry = Instantiate(
            pickup,
            interactor.pickupPosition.position,
            pickup.transform.rotation,
            interactor.transform
        );

        interactor.carry.transform.localScale = pickup.transform.localScale;
    }
}