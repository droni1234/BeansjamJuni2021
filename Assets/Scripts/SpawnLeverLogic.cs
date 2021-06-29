using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLeverLogic : MonoBehaviour
{

    public float coolDown = 20F;

    [SerializeField]
    private Animator animator;

    private float time = 0F;

    private bool isPulled = false;

    private void Update()
    {
        if (time > 0F)
        {
            time -= Time.deltaTime;
        }
        else if (isPulled)
        {
            isPulled = false;
            animator.SetBool("isDown", isPulled);
        }
    }

    public void PullLever()
    {
        isPulled = true;
        animator.SetBool("isDown", isPulled);
        if(time <= 0F)
        {
            FindObjectOfType<SpawnMaster>().SpawnBodies();
            time = coolDown;
        }
    }

}
