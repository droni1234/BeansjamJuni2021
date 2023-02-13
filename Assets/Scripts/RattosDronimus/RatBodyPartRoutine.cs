using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBodyPartRoutine : MonoBehaviour
{
    public SpawnItem[] targets;

    [SerializeField]
    private SmartRats prefabRat;

    public float coolDown = 5.0F;

    private SpawnItem currentTarget;
    private SmartRats rat;


    private float spawnCoolDownTimer = 0.0F;

    private void Update()
    {
        if (rat)
        {

        }
        else if (spawnCoolDownTimer <= 0.0F && currentTarget)
        {
            SpawnRat();
            spawnCoolDownTimer = coolDown;
        }
        else
        {
            AquireNewTarget();
        }

        spawnCoolDownTimer -= Time.deltaTime;
    }

    void SpawnRat()
    {
        rat = Instantiate(prefabRat, transform);
        rat.OnTargetReached += OnTargetReachedBodyPart;
        rat.setTarget(currentTarget.transform.position);
    }

    private void OnTargetReachedBodyPart()
    {
        print("reached");
        currentTarget.CleanUp();
        if (currentTarget.pickup)
        {
            rat.item = Instantiate(
                currentTarget.pickup, 
                rat.transform.position, 
                currentTarget.transform.rotation,
                rat.transform);
        }
        rat.OnTargetReached -= OnTargetReachedBodyPart;
        rat.OnTargetReached += OnTargetReachedOrigin;
        rat.setTarget(transform.position);
    }

    private void OnTargetReachedOrigin()
    {
        spawnCoolDownTimer = coolDown;
        rat.OnTargetReached -= OnTargetReachedOrigin;
        Destroy(rat.gameObject);
    }

    void AquireNewTarget()
    {
        currentTarget = targets[Random.Range(0, targets.Length)];
        if (currentTarget.pickup) return;
        foreach (SpawnItem spawn in targets)
        {
            if (spawn.pickup)
            {
                currentTarget = spawn;
            }
        }
        currentTarget = null;
    }
}
