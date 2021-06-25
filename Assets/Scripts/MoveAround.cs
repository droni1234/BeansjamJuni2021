using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    public float speed = 1.0f;

    private Rigidbody2D _body;
    
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var dT = Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var transformPosition = _body.position;
            transformPosition.x -= speed * dT;
            _body.position = transformPosition;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            var transformPosition = _body.position;
            transformPosition.x += speed * dT;
            _body.position = transformPosition;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            var transformPosition = _body.position;
            transformPosition.y += speed * dT;
            _body.position = transformPosition;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            var transformPosition = _body.position;
            transformPosition.y -= speed * dT;
            _body.position = transformPosition;
        }
    }
}