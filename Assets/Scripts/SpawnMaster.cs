using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaster : MonoBehaviour
{

    public void SpawnBodies()
    {
        foreach (Transform child in transform) 
        {
            Spawn spawn = child.GetComponent<Spawn>();
            if (spawn)
            {
                StartCoroutine(DelaySpawn(spawn));
            }
        }
    }

    private IEnumerator DelaySpawn(Spawn spawn)
    {
        yield return new WaitForSeconds(Random.Range(0.2F,1.0F));
        spawn.SpawnRandomBodyPart();
    }
}
