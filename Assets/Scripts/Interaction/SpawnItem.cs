using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : Item
{

    public GameObject[] possibleSpawns;

    public void SpawnRandomBodyPart ()
    {
        CleanUp();

        pickup = Instantiate(GetRandomBodyPart(), transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0F, 360F))),transform);
    }

    public void CleanUp()
    {
        Destroy(pickup);
    }

    private GameObject GetRandomBodyPart()
    {
        return possibleSpawns[Random.Range(0, possibleSpawns.Length)];
    }

    public override void Trigger(Interactor interactor)
    {
        /*if (pickup)
        {
            interactor.DestroyCarriedBodyPart();

            interactor.carry = Instantiate(
                pickup,
                interactor.pickupPosition.position,
                pickup.transform.rotation,
                interactor.transform
            );

            interactor.carry.transform.localScale = pickup.transform.localScale;
        }*/
        base.Trigger(interactor);
        CleanUp();
    }
}
