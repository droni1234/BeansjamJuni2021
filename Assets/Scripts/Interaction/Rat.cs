using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Pausable))]
public class Rat : Interactable
{
    public float moveSpeed = 3f;
    public float idleDelay = 1f;
    public float stealingDelay = 2f;
    public float lookingDelay = 0.25f;
    
    [SerializeField]
    private GameObject ratDead;

    private enum RatState
    {
        Idle,
        Turning,
        Moving,
        Looking,
        Stealing
    }

    public RatWaypoint currentWaypoint;

    private Animator animator;
    private Rigidbody2D body;
    private RatState state;
    private float delayTime;
    private SurgeryTable surgeryTable;
    private Pausable pause;


    private void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        pause = GetComponent<Pausable>();
        
        SetState(RatState.Idle);
        delayTime = 0.0f;
    }

    private void Update()
    {
        if (pause.Paused)
        {
            return;
        }

        switch (state)
        {
            case RatState.Idle:
                IdleState();
                break;
            case RatState.Turning:
                SetState(RatState.Moving);
                break;
            case RatState.Moving:
                MoveState();
                break;
            case RatState.Looking:
                LookingState();
                break;
            case RatState.Stealing:
                StealState();
                break;
            default:
                throw new ArgumentOutOfRangeException("State: "+ state);
        }
    }

    #region StateMethods
    
    private void IdleState()
    {
        delayTime += Time.deltaTime;
        if (delayTime <= idleDelay) 
            return;
        
        SetState(RatState.Turning);
        delayTime = 0.0f;
    }

    private void MoveState()
    {
        Vector2 motion = Vector2.zero;
        Vector2 toTarget = (currentWaypoint.transform.position - transform.position);
        if (toTarget.magnitude > 0.1f)
        {
            Vector2 position = body.position;
            motion = toTarget.normalized * moveSpeed;
            body.position = position + motion * Time.deltaTime;
        }
        else
        {
            motion = (Vector2) currentWaypoint.transform.position - body.position;
            body.position = currentWaypoint.transform.position;
                
            if (currentWaypoint.nextPoint)
            {
                currentWaypoint = currentWaypoint.nextPoint;
                SetState(RatState.Looking);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        UpdateAnimation(motion);
    }

    private void LookingState()
    {
        if (delayTime < lookingDelay)
        {
            delayTime += Time.deltaTime;
            return;
        }

        delayTime = 0.0f;

        SetState(surgeryTable ? RatState.Stealing : RatState.Idle);
    }
    
    private void StealState()
    {
        delayTime += Time.deltaTime;

        if (delayTime <= stealingDelay)
            return;

        if (!surgeryTable)
            return;
        
        surgeryTable.Steal(gameObject.transform);
                
        delayTime = 0.0f;
        SetState(RatState.Idle);
        
    }

    #endregion

    private void UpdateAnimation(Vector2 motion)
    {
        motion.Normalize();
        animator.SetFloat("DX", motion.x);
        animator.SetFloat("DY", motion.y);
    }
    

    private void SetState(RatState state)
    {
        this.state = state;
    }

    public override void Trigger(Interactor interactor)
    {
        if(ratDead)
            Instantiate(ratDead, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}