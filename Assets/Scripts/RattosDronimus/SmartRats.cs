using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SmartRats : Interactable
{

    [SerializeField]
    private GameObject ratDead;
    
    private Animator animator;
    private Rigidbody2D rigidbody2d;

    [HideInInspector]
    public GameObject item;
    private Vector2 lastPosition = Vector2.zero;

    private Vector2 start = Vector2.zero;
    private Vector2 target = Vector2.zero;
    private float distance;
    public bool moving = false;
    private float setTargetTime;

    public float speed = 1.0F;
    public float targetProximity = 0.1F;
    public delegate void targetReached();
    public event targetReached OnTargetReached;
    
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckRatPosition();
        UpdateRatPosition();
        UpdateAnimation(new Vector2(transform.position.x, transform.position.y) - lastPosition);
        lastPosition = transform.position;
    }

    public void StopRatMovement()
    {
        moving = false;
    }
    
    private void CheckRatPosition()
    {
        // ReSharper disable once InvertIf
        if (moving && Vector2.Distance(transform.position, target) < targetProximity)
        {
            moving = false;
            OnTargetReached.Invoke();
        }
    }

    private void UpdateAnimation(Vector2 motion)
    {
        motion.Normalize();
        animator.SetFloat("DX", motion.x);
        animator.SetFloat("DY", motion.y);
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public void setTarget(Vector2 target)
    {
        print("new target aquired");
        start =transform.position;
        this.target = target;
        moving = true;
        setTargetTime = Time.time;
        distance = Vector2.Distance(start, target);
    }

    private void UpdateRatPosition()
    {
        if(!moving) return;
        //Long ass formula depending on speed and distance
            rigidbody2d.position = Vector2.Lerp(start,
                target,
                (Time.time - setTargetTime) * speed /
                distance
                );
        }

    public override void Trigger(Interactor interactor)
    {
        Die();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Die()
    {
        Instantiate(ratDead, transform.position, Quaternion.identity, null);
        if (item)
        {
            Item.spawnItem(item, 10);
        }

        Destroy(gameObject);
    }
}
