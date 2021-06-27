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
                spawn.SpawnRandomBodyPart();
            }
        }
    }
}
