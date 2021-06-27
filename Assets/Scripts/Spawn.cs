using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject[] possibleSpawns;

    private GameObject bodyPart;

    public void SpawnRandomBodyPart ()
    {
        CleanUp();

        bodyPart = Instantiate(GetRandomBodyPart(), transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0F, 360F))),transform);

        GetComponent<Interactable>().pickup = bodyPart;
    }

    public void CleanUp()
    {
        Destroy(bodyPart);
    }

    private GameObject GetRandomBodyPart()
    {
        return possibleSpawns[Random.Range(0, possibleSpawns.Length)];
    }
}
