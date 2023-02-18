using System;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation


public class Item : Interactable
{
    public GameObject pickup;

    protected bool destroyOnPickup = true;

    public static void spawnItem(GameObject pickupItem, float decayTime = -1)
    {
        var me = new GameObject("Item");
        me.transform.position = pickupItem.transform.position;
        me.AddComponent<BoxCollider2D>().isTrigger = true;
        var item = me.AddComponent<Item>();
        item.pickup = Instantiate(pickupItem, pickupItem.transform.position, pickupItem.transform.rotation, me.transform);

        if (decayTime > 0)
        {
            Destroy(me, decayTime);
        }
    }

    public override void Trigger(Interactor interactor)
    {
        if (!pickup) 
            return;

        if (interactor.carry)
        {
            spawnItem(interactor.carry, 10);
        }
        
        interactor.DestroyCarriedBodyPart();

        interactor.carry = Instantiate(
            pickup,
            interactor.pickupPosition.position,
            pickup.transform.rotation,
            interactor.transform
        );

        interactor.carry.transform.localScale = pickup.transform.localScale;
        if(destroyOnPickup)
            Destroy(this.gameObject);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Destroy(pickup);
    }
}