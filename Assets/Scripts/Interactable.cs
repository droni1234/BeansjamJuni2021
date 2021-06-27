using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Interactable : MonoBehaviour
{
    public GameObject pickup;
    public float minAlpha = 0.7f;
    public float maxAlpha = 1.0f;
    public float alphaChange = 0.2f;
    public float actionCost = 0.0f;
    
    private bool _fadeIn = false;

    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        var color = _renderer.color;
        color.a = minAlpha;
        _renderer.color = color;

        if (pickup)
        {
            Instantiate(pickup, transform.position, pickup.transform.rotation, transform);
        }
    }

    void Update()
    {
        var dT = Time.deltaTime;
        var color = _renderer.color;
        if (color.a >= minAlpha && !_fadeIn)
        {
            color.a -= alphaChange * dT;
        }
        else if (color.a <= maxAlpha && _fadeIn)
        {
            color.a += alphaChange * dT;
        }
        
        _renderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactor = other.GetComponent<Interactor>();
        if (!interactor)
        {
            return;
        }

        _fadeIn = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactor = other.GetComponent<Interactor>();
        if (!interactor)
        {
            return;
        }

        _fadeIn = false;
    }
}