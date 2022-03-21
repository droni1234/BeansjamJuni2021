using System.Collections;
using System.Collections.Generic;
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

    private Vector2 lastPosition = Vector2.zero;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateAnimation(new Vector2(transform.position.x, transform.position.y) - lastPosition);
        //UpdateAnimation(rigidbody2d.velocity);
        lastPosition = transform.position;
    }

    private void UpdateAnimation(Vector2 motion)
    {
        animator.SetFloat("DX", motion.x);
        animator.SetFloat("DY", motion.y);
    }

    public override void Trigger(Interactor interactor)
    {
        Instantiate(ratDead, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
