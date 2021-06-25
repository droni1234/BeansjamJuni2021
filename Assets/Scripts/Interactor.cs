using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private GameObject current;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (current)
            {
                var renderer = current.GetComponent<SpriteRenderer>();
                renderer.color = Color.red;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        current = other.gameObject;
    }
}
