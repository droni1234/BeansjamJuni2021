using System;
using System.Collections;
using System.Collections.Generic;
using RattosDronimus;
using UnityEngine;
using Random = UnityEngine.Random;

public class LabRat : MonoBehaviour
{
    
    [SerializeField]
    private SmartRats prefabRat;

    public float speed = 1.5F;
    
    public float coolDown = 15.0F;

    [SerializeField] 
    private Waypoint rootWaypoint;
    
    [SerializeField]
    private Waypoint currentTarget;
    [SerializeField]
    private SurgeryTable surgeryTable;
    private SmartRats rat;
    
    private float spawnCoolDownTimer = 0.0F;

    private void Update()
    {
        if (rat)
        {

        }
        else if (spawnCoolDownTimer <= 0.0F)
        {
            currentTarget = rootWaypoint;
            SpawnRat();
            spawnCoolDownTimer = coolDown;
        }

        spawnCoolDownTimer -= Time.deltaTime;
    }
    
    void SpawnRat()
    {
        rat = Instantiate(prefabRat, currentTarget.transform.position, Quaternion.identity, transform);
        rat.speed = speed;
        rat.OnTargetReached += OnTargetReached;
        AcquireNextWaypoint();
        rat.setTarget(currentTarget.transform.position);
    }

    private void OnTargetReached()
    {
        if (currentTarget.isSurgeryTable) {
            surgeryTable.Steal(rat);
        }

        if (!AcquireNextWaypoint())
        {
            currentTarget = null;
        }
        
        if (!currentTarget)
        {
            rat.OnTargetReached -= OnTargetReached;
            spawnCoolDownTimer = coolDown;
            currentTarget = null;
            Destroy(rat.gameObject);
            return;
        }
        
        rat.setTarget(currentTarget.transform.position);
    }

    void GetRootWaypoint()
    {
        var waypoint = GetComponentsInChildren<Waypoint>();

        currentTarget = waypoint[Random.Range(0,waypoint.Length)];
    }
    
    bool AcquireNextWaypoint()
    {
        if (currentTarget.transform.childCount == 0)
            return false;
        
        int nextChildIndex = Random.Range(0, currentTarget.transform.childCount);
        var nextWaypoint = currentTarget.transform.GetChild(nextChildIndex);
        currentTarget = nextWaypoint.GetComponent<Waypoint>();
        return true;
    }
}
