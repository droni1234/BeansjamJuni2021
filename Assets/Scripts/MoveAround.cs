using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    public float speed = 1.0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var dT = Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var transformPosition = transform.position;
            transformPosition.x -= speed * dT;
            transform.position = transformPosition;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            var transformPosition = transform.position;
            transformPosition.x += speed * dT;
            transform.position = transformPosition;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            var transformPosition = transform.position;
            transformPosition.y += speed * dT;
            transform.position = transformPosition;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            var transformPosition = transform.position;
            transformPosition.y -= speed * dT;
            transform.position = transformPosition;
        }
    }
}