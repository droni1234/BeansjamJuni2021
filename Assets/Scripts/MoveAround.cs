using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    public float speed = 1.0f;

    private AudioSource _audio;
    private Animator _animator;
    private Rigidbody2D _body;
    private string _animState = "Stay";
    
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        
        _animator.SetBool(_animState, true);
    }

    // Update is called once per frame
    void Update()
    {
        var dT = Time.deltaTime;
        var moved = false;
        var state = _animState;
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        if (xAxis < 0)
        {
            var transformPosition = _body.position;
            transformPosition.x += speed * dT * xAxis;
            _body.position = transformPosition;
            state = "Left";
            moved = true;
        }
        else if (xAxis > 0)
        {
            var transformPosition = _body.position;
            transformPosition.x += speed * dT * xAxis;
            _body.position = transformPosition;
            state = "Right";
            moved = true;
        }
        
        if (yAxis > 0)
        {
            var transformPosition = _body.position;
            transformPosition.y += speed * dT * yAxis;
            _body.position = transformPosition;
            state = "Away";
            moved = true;
        }
        else if (yAxis < 0)
        {
            var transformPosition = _body.position;
            transformPosition.y += speed * dT * yAxis;
            _body.position = transformPosition;
            state = "Towards";
            moved = true;
        }

        if (!moved)
        {
            state = "Stay";
            
            _audio.loop = false;
            _audio.Stop();
        }
        else
        {
            if (!_audio.loop)
            {
                _audio.loop = true;
                _audio.Play();
            }
        }

        if (!_animState.Equals(state))
        {
            //Debug.Log("Setting state: " + state);
            _animState = state;
            _animator.SetBool(_animState, true);

            if (!_animState.Equals("Stay"))
            {
                _animator.SetBool("Stay", false);
            }
        }
    }
}