using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Rat : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float moveSpeed = 5f;
    
    public RatWaypoint currentWaypoint;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = currentWaypoint.transform.position - transform.position;
        Vector3 facing = transform.up;
        var facingAngle = Vector3.Angle(facing, toTarget);
        Debug.Log("facing angle: " + facingAngle);
        if (Math.Abs(facingAngle) > rotationSpeed / 60.0f) // TODO: calc FPS instead of this wrong estimate
        {
            // Rotate gradually
            var sign = facingAngle / Math.Abs(facingAngle);
            transform.Rotate(transform.forward, sign * rotationSpeed * Time.deltaTime);
            
            return;
        }
        /*
        else if (facingAngle != 0.0f)
        {
            // Finish rotating to 0 degrees
            transform.Rotate(transform.forward, facingAngle);
            return;
        }*/

        if (toTarget.magnitude > 0.5f)
        {
            // TODO: We should use physics RigidBody2D here as well once Rat has one...
            var position = transform.position;
            position = position + transform.up.normalized * (moveSpeed * Time.deltaTime);
            transform.position = position;
            return;
        }

        if (currentWaypoint.nextPoint != null)
        {
            currentWaypoint = currentWaypoint.nextPoint;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
