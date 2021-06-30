using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBodyPartRoutine : MonoBehaviour
{
    public Spawn[] targets;

    [SerializeField]
    private SmartRats prefabRat;
    
    [SerializeField]
    private float speed = 1.0F;

    public float coolDown = 5.0F;

    private Spawn currentTarget;
    private SmartRats rat;
    
    private float startTime;


    private float coolDownTimer = 0.0F;

    private float tolerance = 0.1F;

    void Update()
    {
        if (rat)
        { 
            UpdateRatPosition();
            TryEatTarget();
        }
        else if (coolDownTimer <= 0.0F)
        {
            AquireNewTarget();
            if(currentTarget)
                SpawnRat();
            coolDownTimer = coolDown;
        }

        coolDownTimer -= Time.deltaTime;
    }

    private void UpdateRatPosition()
    {
        //Long ass formular depending on speed and distance to item it will move accordingly
        rat.GetComponent<Rigidbody2D>().position = Vector2.Lerp(transform.position, currentTarget.transform.position, (Time.time - startTime) * speed / Vector2.Distance(transform.position, currentTarget.transform.position));
    }

    void SpawnRat()
    {
        rat = Instantiate(prefabRat, transform);
    }

    void TryEatTarget()
    {
        if (Vector2.Distance(rat.transform.position, currentTarget.transform.position) < tolerance)
        {
            coolDownTimer = coolDown;
            currentTarget.CleanUp();
            Destroy(rat.gameObject);
        }
    }

    void AquireNewTarget()
    {
        currentTarget = targets[Random.Range(0, targets.Length)];
        if (!currentTarget.bodyPart)
        {
            foreach (Spawn spawn in targets)
            {
                if (spawn.bodyPart)
                {
                    currentTarget = spawn;
                }
            }
            currentTarget = null;
        }
        startTime = Time.time;
    }
}
