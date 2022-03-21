using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaster : MonoBehaviour
{

    public void SpawnBodies()
    {
        foreach (Transform child in transform) 
        {
            SpawnItem spawnItem = child.GetComponent<SpawnItem>();
            if (spawnItem)
            {
                StartCoroutine(DelaySpawn(spawnItem));
            }
        }
    }

    private static IEnumerator DelaySpawn(SpawnItem spawnItem)
    {
        yield return new WaitForSeconds(Random.Range(0.2F,1.0F));
        spawnItem.SpawnRandomBodyPart();
    }
}
